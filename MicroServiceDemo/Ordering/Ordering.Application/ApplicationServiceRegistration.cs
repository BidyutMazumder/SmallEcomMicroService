using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contacts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Ping).Assembly));
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
            });
            services.AddFluentValidationAutoValidation();


            return services;
        }
    }
}
