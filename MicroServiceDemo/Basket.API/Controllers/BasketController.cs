using AutoMapper;
using Basket.API.GrpcServices;
using Basket.API.Models;
using Basket.API.Repositories.Abstraction;
using CoreApiResponse;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBusketRepository _busketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        public BasketController(
            IBusketRepository busketRepository, 
            DiscountGrpcService discountGrpcService, 
            IPublishEndpoint publishEndpoint,
            IMapper mapper
            )
        {
            this._busketRepository = busketRepository;
            this._discountGrpcService = discountGrpcService;
            this._publishEndpoint = publishEndpoint;
            this._mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ShopingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult>GetBasket([FromQuery]string userName)
        {
            try
            {
                var Basket = await _busketRepository.GetBasket(userName);
                return CustomResult("Basket data load successfully", Basket, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(ShopingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShopingCart basket)
        {
            try
            {
                foreach (var item in basket.items)
                {
                    var coupon = await _discountGrpcService.GetDiscount(item.ProductId);
                    item.Price -= coupon.Amount;
                }
                var Basket = await _busketRepository.UpdateBasket(basket);
                return CustomResult("Product add to cart Successfully", Basket, HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket([FromQuery] string userName)
        {
            try
            {
                await _busketRepository.DeleteBasket(userName);
                return CustomResult("Basket has been remove successfully", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }


        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _busketRepository.GetBasket(basketCheckout.UserName);
            if (basket == null)
            {
                return CustomResult("Basket is Emply", HttpStatusCode.BadRequest);
            }
            //send checkout event to rabbitmq
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            // remove basket
            await _busketRepository.DeleteBasket(basket.UserName);
            return CustomResult("Order has been placed successfully.");

        }
    }
}
