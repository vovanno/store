using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Configuration
{
    public class Configuration
    {
        public static void RegisterDependencies(IServiceCollection services, string connection)
        {
            services.AddScoped<UserManager<IdentityUser>>();
            DAL.Configuration.Configuration.RegisterDependencies(services, connection);
        }
    }
}
