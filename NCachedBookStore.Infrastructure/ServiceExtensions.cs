using Alachisoft.NCache.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NCachedBookStore.Contracts.Services;
using NCachedBookStore.Infrastructure.Services;

namespace NCachedBookStore.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(optionsBuilder =>
            {
                string cacheId = configuration["CacheId"];

                NCacheConfiguration.Configure(cacheId, DependencyType.SqlServer);
                NCacheConfiguration.ConfigureLogger();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services.AddScoped<IDataService, DataService>();
        }
    }
}
