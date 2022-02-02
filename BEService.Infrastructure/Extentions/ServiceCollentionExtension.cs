using BEService.Core.CustomEntities.Options;
using BEService.Core.Interfaces.Repositories;
using BEService.Core.Interfaces.Services.MainServices;
using BEService.Core.Interfaces.Services.SupportServices;
using BEService.Core.Interfaces.Unit;
using BEService.Core.Services.MainServices;
using BEService.Core.Services.SupportServices;
using BEService.Infrastructure.Data;
using BEService.Infrastructure.Repositories;
using BEService.Infrastructure.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BEService.Infrastructure.Extentions
{
    public static class ServiceCollentionExtension
    {
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BEDBContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("connection")),
                ServiceLifetime.Transient);
        }

        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));
            services.Configure<TokenOptions>(options => configuration.GetSection("TokenOptions").Bind(options));
            services.Configure<PaginationOptions>(options => configuration.GetSection("PaginationOptions").Bind(options));
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Repository
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //Main Service
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMenuAppService, MenuAppService>();
            services.AddTransient<IMenuAccessService, MenuAccessService>();

            //Support Service
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<ITokenService, TokenService>();

            //Unit
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Service API",
                    Version = "v1"
                });

                doc.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using bearer scheme."
                });

                doc.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });

            return services;
        }
    }
}
