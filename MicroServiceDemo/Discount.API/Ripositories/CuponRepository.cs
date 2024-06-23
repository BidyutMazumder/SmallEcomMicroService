using Dapper;
using Discount.API.Models;
using Discount.API.Ripositories.Abstraction;
using Npgsql;

namespace Discount.API.Ripositories
{
    public class CuponRepository : ICuponRepository
    {
        IConfiguration _configuration;

        public CuponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscount(string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            var Connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var Coupon = await Connection.QueryFirstOrDefaultAsync<Coupon>
                ("select * from Coupon where ProductId = @ProductId", new { ProductId = productId });
            if (Coupon == null)
            {
                return 
            }
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
