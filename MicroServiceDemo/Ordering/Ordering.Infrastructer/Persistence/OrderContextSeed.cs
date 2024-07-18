using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructer.Persistence
{
    public class OrderContextSeed
    {
        public static async Task Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData
                (
                    new Order()
                    {
                        Id = 1,
                        UserName = "Hriday.mder31@gmail.com",
                        TotalPrice = 100,
                        FirstName = "Bidyut",
                        LastName = "Mazumder",
                        EmailAddress = "Hriday.mder31@gmail.com",
                        Address = "Dhaka",
                        City = "Dhaka",
                    }
                );
        }
    }
}
