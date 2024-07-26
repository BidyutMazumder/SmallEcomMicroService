using AutoMapper;
using MediatR;
using Ordering.Application.Contacts.Infrastructure;
using Ordering.Application.Contacts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Models;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService)
        {
            this._orderRepository = orderRepository;
            this._mapper = mapper;
            this._emailService = emailService;
        }
        public async Task<bool>Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            //dumy data
            order.CreatedBy = "1";
            order.CreatedOn = DateTime.Now;
            //-------------------
            bool isOrderPlaced = await _orderRepository.AddAsync(order);
            if (isOrderPlaced)
            {
                EmailMessage email = new EmailMessage
                {
                 To = order.EmailAddress,
                 Subject = "Your order has been placed",
                 Body = $"Dear {order.FirstName} {order.LastName} <br/> <br/>"
                };

                await _emailService.SendEmailAsync(email);
            }
            return isOrderPlaced;
        }
    }
}
