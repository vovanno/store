using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppContext = DAL.Context.AppContext;

namespace DAL.Configuration
{
    public class Configuration
    {
        public static void RegisterDependencies(IServiceCollection services, string connection)
        {
            services.AddDbContext<AppContext>(opt => opt.UseSqlServer(connection));
            services.AddScoped(typeof(IBaseRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppContext, AppContext>();
            services.AddIdentity<IdentityUser, IdentityRole>(opt => opt.Password = new PasswordOptions()
            {
                RequireDigit = true,
                RequiredLength = 6,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonAlphanumeric = true
            })
                .AddEntityFrameworkStores<AppContext>();
        }
    }
}
