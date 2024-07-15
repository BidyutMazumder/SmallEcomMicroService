using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }
        public async Task<CouponRequest> GetDiscount(string productId)
        {
            var discountData = new GetDiscountRequest { ProductId = productId };
            return await _discountProtoService.GetDiscountAsync(discountData);
        }
    }
}
