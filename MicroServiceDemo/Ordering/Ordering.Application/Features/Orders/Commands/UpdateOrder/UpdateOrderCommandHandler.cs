﻿using AutoMapper;
using MediatR;
using Ordering.Application.Contacts.Persistence;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this._orderRepository = orderRepository;
            this._mapper = mapper;
        }
        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            //dumy data
            order.UpdatedBy = "1";
            order.UpdatedOn = DateTime.Now; 
            //-----------------
            return await _orderRepository.UpdateAsync(order);
        }
    }
}
