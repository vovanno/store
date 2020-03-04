using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Sockets;
using AppContext = DAL.Context.AppContext;

namespace DAL.Configuration
{
    public class Configuration
    {
        public static void RegisterDependencies(IServiceCollection services, string connection)
        {
            services.AddDbContext<AppContext>(opt => opt.UseMySql(connection));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IAppContext, AppContext>();
            services.AddIdentity<IdentityUser, IdentityRole>(opt => opt.Password = new PasswordOptions()
            {
                RequireDigit = true,
                RequiredLength = 6,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonAlphanumeric = true
            })
                .AddEntityFrameworkStores<AppContext>();
            TryMigrate(connection);
        }

        public static void TryMigrate(string connection)
        {
            var configurationArray = connection.ToLower().Split(';');
            var host = configurationArray.Single(p => p.StartsWith("server", StringComparison.CurrentCultureIgnoreCase))
                .TrimStart("server=".ToCharArray());
            var port = Convert.ToInt32(configurationArray.Single(p => p.StartsWith("port", StringComparison.CurrentCultureIgnoreCase))
                .TrimStart("port=".ToCharArray()));

            try
            {
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.Connect(host, port);
                    Console.WriteLine("Successfully connected to database");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection to database failed.\n" + e.Message);
            }

            var builder = new DbContextOptionsBuilder<AppContext>().UseMySql(connection);
            var context = new AppContext(builder.Options);
            context.Database.Migrate();
        }
    }
}
