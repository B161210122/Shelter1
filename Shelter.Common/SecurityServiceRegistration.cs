using Microsoft.Extensions.DependencyInjection;
using Shelter.Common.JWT;

namespace Shelter.Common
{
    public static class SecurityServiceRegistration
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHelper, JwtHelper>();
            return services;
        }
    }
}
