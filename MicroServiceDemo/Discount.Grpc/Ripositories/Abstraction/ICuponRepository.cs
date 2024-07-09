using Discount.Grpc.Models;

namespace Discount.Grpc.Ripositories.Abstraction
{
    public interface ICuponRepository
    {
        Task<Coupon> GetDiscount(string productId);
        Task<bool>CreateDiscount(Coupon coupon);
        Task<bool>UpdateDiscount(Coupon coupon);
        Task<bool>DeleteDiscount(string productId);
    }
}
