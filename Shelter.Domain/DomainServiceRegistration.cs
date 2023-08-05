using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shelter.Domain.Context;

namespace Shelter.Domain
{
    public static class DomainServiceRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options =>
                                                options.UseSqlServer(
                                                    configuration.GetConnectionString("MyWebsiteConnectionString")));

            return services;
        }
    }
}
