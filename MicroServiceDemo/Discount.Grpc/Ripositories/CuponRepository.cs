using Dapper;
using Discount.Grpc.Models;
using Discount.Grpc.Ripositories.Abstraction;
using Npgsql;

namespace Discount.Grpc.Ripositories
{
    public class CuponRepository : ICuponRepository
    {
        IConfiguration _configuration;

        public CuponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var Connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var Affected = await Connection.ExecuteAsync
                ("INSERT INTO public.coupon(productid, productname, description, amount)VALUES (@productid, @productname, @description, @amount)",
                new { productid = coupon.ProductId, productname = coupon.ProductName, description = coupon.Description, amount = coupon.Amount });
            if (Affected > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<bool> DeleteDiscount(string productId)
        {
            var Connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var Affected = await Connection.ExecuteAsync
                ("DELETE FROM public.coupon WHERE productid = @productid;",
                new { productid = productId});
            if (Affected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            var Connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var Coupon = await Connection.QueryFirstOrDefaultAsync<Coupon>
                ("select * from Coupon where ProductId = @ProductId", new { ProductId = productId });
            if (Coupon == null)
            {
                return new Coupon { Amount = 0, ProductName = "No Discount", ProductId = "No Discount", Description = "No Discount" };
            }
            return Coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var Connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDB"));
            var Affected = await Connection.ExecuteAsync
                ("UPDATE public.coupon SET productid=@productid, productname=@productname, description=@description, amount=@amount WHERE productId = @productId;)",
                new { productid = coupon.ProductId, productname = coupon.ProductName, description = coupon.Description, amount = coupon.Amount });
            if (Affected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
