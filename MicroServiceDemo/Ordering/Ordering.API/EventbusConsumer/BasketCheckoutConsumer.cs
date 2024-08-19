using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CreateOrder;

namespace Ordering.API.EventbusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;
        private readonly IMapper _mapper;


        public BasketCheckoutConsumer(IMediator mediator, 
            ILogger<BasketCheckoutConsumer> logger,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
             var orderData = _mapper.Map<CreateOrderCommand>(context.Message);

             bool isOrderConfirm = await _mediator.Send(orderData);

            if (isOrderConfirm)
            {
                _logger.LogInformation($"Basket checkout event has been consumd. order username: {orderData.UserName}");
            }
            else
            {
                _logger.LogInformation($"basket checkout event failed for {orderData.UserName}");
            }
            
            
        }
    }
}
