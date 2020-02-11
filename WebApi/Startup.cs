using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using WebApi.Configuration.Mapping;
using WebApi.Configuration.Validation;
using WebApi.Middleware;
using WebApi.VIewDto;

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
            var mapConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:Audience"],
                        ValidateLifetime = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("user", policyBuilder =>
                     policyBuilder.RequireClaim(ClaimTypes.Role, "user", "manager", "admin", "publisher", "moderator"));
            });

            
            var mapper = mapConfig.CreateMapper();
            services.AddSingleton(mapper);

            var authPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();
            services.AddMvc(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(authPolicy));
                
            })
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<CustomValidationInterceptor>());
            services.AddScoped<IValidator<GameViewDto>, GameValidator>();
            services.AddScoped<IValidator<CommentViewDto>, CommentValidator>();
            services.AddScoped<IValidator<GenreViewDto>, GenreValidator>();
            services.AddScoped<IValidator<PublisherViewDto>, PublisherValidator>();
            services.AddScoped<IValidator<PlatformViewDto>, PlatformValidator>();
            services.AddScoped<IValidator<UserViewDto>, UserValidator>();
            services.AddScoped<IValidator<OrderViewDto>, OrderValidator>();
            services.AddScoped<IValidatorInterceptor, CustomValidationInterceptor>();

            services.AddSingleton<IAmazonDynamoDB>(provider => new AmazonDynamoDBClient(RegionEndpoint.EUWest2));
            services.AddSingleton<IDynamoDBContext>(provider =>
                new DynamoDBContext(provider.GetRequiredService<IAmazonDynamoDB>()));

            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPlatformService, PlatformTypeService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<ISubGenreService, SubGenreService>();
            BLL.Configuration.Configuration.RegisterDependencies(services, Configuration.GetConnectionString("DefaultConnection"));
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "Online GameStore", Version = "v1" })
            );
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
            app.UseAuthentication();
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
