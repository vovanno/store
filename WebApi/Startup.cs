using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BLL.Interfaces;
using BLL.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;
using Amazon.S3;
using WebApi.Configuration;
using WebApi.Configuration.Validation;
using WebApi.Middleware;

namespace WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CustomPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            //    {
            //        opt.RequireHttpsMetadata = false;
            //        opt.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateIssuer = true,
            //            ValidIssuer = Configuration["JWT:Issuer"],
            //            ValidateAudience = true,
            //            ValidAudience = Configuration["JWT:Audience"],
            //            ValidateLifetime = false,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
            //            ValidateIssuerSigningKey = true,
            //        };
            //    });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("user", policyBuilder =>
            //         policyBuilder.RequireClaim(ClaimTypes.Role, "user", "manager", "admin", "publisher", "moderator"));
            //});
            
            var authPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();
            services.AddMvc(opt =>
            {
                //opt.Filters.Add(new AuthorizeFilter(authPolicy));
                
            })
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<CustomValidationInterceptor>());
            services.AddScoped<IValidatorInterceptor, CustomValidationInterceptor>();

            services.AddSingleton<IAmazonDynamoDB>(provider => new AmazonDynamoDBClient(RegionEndpoint.EUWest2));
            services.AddSingleton<IDynamoDBContext>(provider =>
                new DynamoDBContext(provider.GetRequiredService<IAmazonDynamoDB>()));
            services.AddSingleton<IAmazonS3>(provider => new AmazonS3Client());

            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            BLL.Configuration.Configuration.RegisterDependencies(services, Configuration.GetConnectionString("DefaultConnection"));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info {Title = "Online GameStore", Version = "v1"});
                options.OperationFilter<FormFileSwaggerFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CustomPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Online GameStore")
            );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseAuthentication();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
        }
    }
}
