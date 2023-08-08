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
            //services.AddDbContext<BaseDbContext>(options =>
            //                                    options.UseSqlServer(
            //                                        configuration.GetConnectionString("MyWebsiteConnectionString")));
            services.AddDbContext<BaseDbContext>(options =>
                                               options.UseSqlServer(
                                                 "Server=localhost,11453;Initial Catalog=ShelterDB;Persist Security Info=True;User ID=sa;Password=Pswrd12345.;MultipleActiveResultSets=True;TrustServerCertificate=True;"));

            return services;
        }
    }
}
