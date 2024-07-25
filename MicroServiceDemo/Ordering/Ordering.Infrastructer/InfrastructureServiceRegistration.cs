using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contacts.Infrastructure;
using Ordering.Application.Contacts.Persistence;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructer
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(option => option
            .UseSqlServer(configuration
            .GetConnectionString("dbConnection")));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
