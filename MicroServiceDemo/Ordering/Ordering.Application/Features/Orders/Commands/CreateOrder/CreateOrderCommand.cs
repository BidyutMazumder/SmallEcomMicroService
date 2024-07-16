﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand: IRequest<bool>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Decimal Amount { get; set; }

        //billing address

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Stat { get; set; }
        public string ZipCode { get; set; }

        // payment

        public string CardNamer { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string Exprition { get; set; }
        public int PaymentMethod { get; set; }
    }
}
