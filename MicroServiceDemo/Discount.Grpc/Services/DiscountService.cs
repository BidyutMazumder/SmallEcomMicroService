using Discount.Grpc.Protos;
using Discount.Grpc.Ripositories.Abstraction;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICuponRepository _cuponRepository;
        private readonly ILogger<DiscountService> _logger;
        public DiscountService(ICuponRepository cuponRepository, ILogger<DiscountService> logger) 
        {
            this._cuponRepository = cuponRepository;
            this._logger = logger;
        }
        

    }
}
