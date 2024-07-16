using AutoMapper;
using MediatR;
using Ordering.Application.Contacts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByUserName
{
    public class GetOrdersByUserHandler : IRequestHandler<GetOrdersByUserQuery, List<OrderVm>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByUserHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            this._mapper = mapper;
            this._orderRepository = orderRepository;
        }
        public async Task<List<OrderVm>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrdersByUserName(request.UserName);

            return _mapper.Map<List<OrderVm>>(order);
        }
    }
}
