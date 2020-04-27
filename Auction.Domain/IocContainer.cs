using Domain.Commands;
using Domain.Events;
using Domain.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Auction.Domain
{
    public static class IocContainer
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => t => sp.GetService(t));
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();

            return services;
        }
    }
}
