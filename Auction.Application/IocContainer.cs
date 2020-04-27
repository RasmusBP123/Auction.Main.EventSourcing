using Auction.Application.CreateItem;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auction.Application
{
    public static class IocContainer
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
             services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddScoped<IRequestHandler<CreateItemCommand>, CreateItemHandler>();

            return services;
        }
    }
}
