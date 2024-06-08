using Basket.API.Models;

namespace Basket.API.Repositories.Abstraction
{
    public interface IBusketRepository
    {
        Task<ShopingCart> GetBasket(string userName);
        Task<ShopingCart> UpdateBasket(ShopingCart basket);
        Task DeleteBasket(string userName);


    }
}
