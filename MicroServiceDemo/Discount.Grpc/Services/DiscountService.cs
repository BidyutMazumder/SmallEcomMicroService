using Discount.Grpc.Protos;
using Discount.Grpc.Ripositories.Abstraction;
using Grpc.Core;

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
        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = _cuponRepository.GetDiscount(request.ProductId);

            return new CouponRequest { ProductId = request.ProductId, PorductName = Coupon };

            /*
            int32 id = 1;
            string product_id = 2;
            string porduct_name = 3;
            string description = 4;
            int32 amount = 5;
             * */
            //return base.GetDiscount(request, context);
        }


    }
}
