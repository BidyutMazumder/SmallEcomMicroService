
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contacts.Persistence;
using Ordering.Domain.Models;
using Ordering.Infrastructer.Persistence;

namespace Ordering.Infrastructer.Repository
{
    public class OrderRepository : CommonRepository<Order>, IOrderRepository
    {
        private readonly OrderDbContext _db;
        public OrderRepository(OrderDbContext dbContext): base(dbContext) 
        {
            this._db = dbContext;
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _db.Orders.Where(c => c.UserName.ToLower() == userName.ToLower()).ToListAsync();   
            return orderList;
        }
    }
}
