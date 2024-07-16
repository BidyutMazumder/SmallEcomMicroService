using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByUserName
{
    public class GetOrdersByUserHandler: IRequestExceptionHandler<GetOrdersByUserQuery, List<OrderVm>>
    {

    }
}
