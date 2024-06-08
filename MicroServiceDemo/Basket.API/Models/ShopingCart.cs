namespace Basket.API.Models
{
    public class ShopingCart
    {
        public ShopingCart(string userName) 
        { 
            UserName = userName;
        }
        public ShopingCart() { }
        public string UserName { get; set; }
        public List<ShopingCartItem> items = new List<ShopingCartItem>();
        public decimal TotalPrice { get 
            {
                decimal totalPrice = 0;
                foreach (var item in items)
                {
                    totalPrice += item.Price;
                }
                return totalPrice; 
            } 
        }
    }
}
