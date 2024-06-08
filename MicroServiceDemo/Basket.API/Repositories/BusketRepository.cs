using Basket.API.Models;
using Basket.API.Repositories.Abstraction;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BusketRepository : IBusketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BusketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
      
        public async Task<ShopingCart> GetBasket(string userName)
        {
            var Basket = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(Basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShopingCart>(Basket);
        }

        public async Task<ShopingCart> UpdateBasket(ShopingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
