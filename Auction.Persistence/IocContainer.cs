using Auction.Core;
using Auction.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Persistence
{
    public static class IocContainer
    {
        public static IServiceCollection RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IEventStore, EventStoreRepository>();
            services.AddDbContext<DbEventStorage>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(DbEventStorage).Assembly.FullName)).UseLazyLoadingProxies());

            return services;
        }
    }
}
