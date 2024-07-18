using CoreApiResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersByUserName;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult>GetOrderByUserName(string userName)
        {
            try 
            { 
                var order = _mediator.Send(new GetOrdersByUserQuery(userName));
                if (order == null)
                {
                    return CustomResult("Order Not Found", HttpStatusCode.NotFound);
                }
                else 
                {
                    return CustomResult("Order Load Successfully", order, HttpStatusCode.OK);
                }
            }
            catch (Exception ex) 
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand orderCommand)
        {
            try
            {
                var orderPlaced = _mediator.Send(orderCommand);
                if (orderPlaced == null)
                {
                    return CustomResult("Order Faield", HttpStatusCode.BadRequest);
                }
                else
                {
                    return CustomResult("Order Placed Successfully", orderPlaced, HttpStatusCode.Accepted);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand updateOrder)
        {
            try
            {
                var orderPlaced = _mediator.Send(updateOrder);
                if (orderPlaced == null)
                {
                    return CustomResult("Update Order Faield", HttpStatusCode.BadRequest);
                }
                else
                {
                    return CustomResult("Update Order Successfully", orderPlaced, HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteOrder(DeleteOrderCommand deleteOrder)
        {
            try
            {
                var orderPlaced = _mediator.Send(deleteOrder);
                if (orderPlaced == null)
                {
                    return CustomResult("Delete Order Faield", HttpStatusCode.BadRequest);
                }
                else
                {
                    return CustomResult("Delete Order Successfully", orderPlaced, HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
