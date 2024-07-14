using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Discount.Grpc.Ripositories.Abstraction;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICuponRepository _cuponRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(ICuponRepository cuponRepository, ILogger<DiscountService> logger, IMapper mapper) 
        {
            this._cuponRepository = cuponRepository;
            this._logger = logger;
            this._mapper = mapper;
        }
        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _cuponRepository.GetDiscount(request.ProductId);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "No discount found"));
            }
            _logger.LogInformation("Discount is Retrive for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);
            /*
            return new CouponRequest {
                ProductId = request.ProductId,
                PorductName = coupon.ProductName, 
                Amount = coupon.Amount, 
                Description = coupon.Description, 
                Id = coupon.Id
            };
            */
            return _mapper.Map<CouponRequest>(coupon);

            /*
            int32 id = 1;
            string product_id = 2;
            string porduct_name = 3;
            string description = 4;
            int32 amount = 5;
             * */
            //return base.GetDiscount(request, context);
        }
        public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool isSaved = await _cuponRepository.CreateDiscount(coupon);
            if (isSaved)
            {
                _logger.LogInformation("Discount is successfully Created ProductName: {productName}", coupon.ProductName);
            }
            else
            {
                _logger.LogInformation("Discount is not Created ProductName: {productName}", coupon.ProductName);
            }
            

            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool isModified = await _cuponRepository.UpdateDiscount(coupon);
            if (isModified)
            {
                _logger.LogInformation("Discount is successfully Updated ProductName: {productName}", coupon.ProductName);
            }
            else
            {
                _logger.LogInformation("Discount Updated failed");
            }
            return _mapper.Map<CouponRequest>(coupon);
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            bool isDeleted = await _cuponRepository.DeleteDiscount(request.ProductId);
            if (isDeleted)
            {
                _logger.LogInformation("Discount is successfully Deleted ProductId: {productId}", request.ProductId);
            }
            else
            {
                _logger.LogInformation("Discount Deleted failed");
            }
            return new DeleteDiscountResponse { Success = isDeleted };
        }

    }
}
