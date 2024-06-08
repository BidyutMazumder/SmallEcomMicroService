using Basket.API.Models;
using Basket.API.Repositories.Abstraction;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        IBusketRepository _busketRepository;
        public BasketController(IBusketRepository busketRepository)
        {
            _busketRepository = busketRepository;
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
    }
}
