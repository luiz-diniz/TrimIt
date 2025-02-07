using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Api.Interfaces;
using ShortUrl.Core;
using ShortUrl.Core.Interfaces;
using ShortUrl.Core.Mappings;
using ShortUrl.Repository;
using ShortUrl.Repository.Interfaces;

namespace ShortUrl.Api.Extensions
{
    public static class BuilderExtensions
    {
        public static void InitializeApplicationDependencies(this WebApplicationBuilder builder)
        {
            AddApplicationServices(builder.Services);
            AddApplicationDatabase(builder);
            AddApplicationRateLimiting(builder);            
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IUrlRepository, UrlRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IReCaptchaValidator, ReCaptchaValidator>();
            services.AddSingleton<IPasswordService, PasswordService>();

            services.AddHostedService<ExpiredUrlService>();

            services.AddAutoMapper(typeof(MappingProfile));
        }

        private static void AddApplicationDatabase(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }

        private static void AddApplicationRateLimiting(WebApplicationBuilder builder)
        {
            builder.Services.Configure<IpRateLimitOptions>(
                builder.Configuration.GetSection("IpRateLimiting"));

            builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            builder.Services.AddInMemoryRateLimiting();
        }
    }
}
