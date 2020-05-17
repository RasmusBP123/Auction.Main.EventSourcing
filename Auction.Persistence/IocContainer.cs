using Auction.Core;
using Auction.Persistence.Factories;
using Auction.Persistence.Repositories;
using Auction.Persistence.Services;
using Marten;
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
            services.AddSingleton<JsonSerializerService>();

            services.AddDbContext<DbEventStorage>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(DbEventStorage).Assembly.FullName)).UseLazyLoadingProxies());
                services.AddSingleton<ISQLConnectionFactory>(new SqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
