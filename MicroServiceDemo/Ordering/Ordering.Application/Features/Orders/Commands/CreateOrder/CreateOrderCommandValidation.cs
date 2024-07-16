using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidation: AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation() 
        { 
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Enter User Name")
                .EmailAddress().WithMessage("Email should be your Username");
            RuleFor(c => c.EmailAddress).NotEmpty().WithMessage("Enter Your Mail");
            RuleFor(c => c.Amount).GreaterThan(0).WithMessage("non negative");
            
        }   
    }
}
