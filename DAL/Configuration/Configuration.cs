using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Sockets;
using AppContext = DAL.Context.AppContext;

namespace DAL.Configuration
{
    public class Configuration
    {
        public static void RegisterDependencies(IServiceCollection services, string connection)
        {
            services.AddDbContext<AppContext>(opt => opt.UseMySql(connection));
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
            TryMigrate(connection);
        }

        public static void TryMigrate(string connection)
        {
            var host = "gamestoreapidatabase.cnn7n8qfjggv.eu-west-2.rds.amazonaws.com";
            var port = 3306;

            try
            {
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.Connect(host, port);

                    //using (var reader = new StreamReader(tcpClient.GetStream()))
                    //{
                    //    var response = reader.ReadToEnd();
                    //    Console.WriteLine(response);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var builder = new DbContextOptionsBuilder<AppContext>().UseMySql(connection);
            var context = new AppContext(builder.Options);
            context.Database.Migrate();

        }
    }
}
