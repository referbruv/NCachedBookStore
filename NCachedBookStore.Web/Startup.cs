using Alachisoft.NCache.Runtime.DatasourceProviders;
using Alachisoft.NCache.ResponseCaching;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCachedBookStore.CachingProviders.Providers;
using NCachedBookStore.Core;
using NCachedBookStore.Infrastructure;
using Alachisoft.NCache.Caching.Distributed;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;

namespace NCachedBookStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCore();

            services.AddRepositories(Configuration);

            services.AddResponseCaching();

            //Add NCache services to the container
            // services.AddNCacheDistributedCache(Configuration.GetSection("NCacheSettings"));

            //Read NCache specific configurations from appsettings.json
            services.AddOptions()
                .Configure<NCacheConfiguration>(Configuration.GetSection("NCacheSettings"));

            //Register NCache for response caching
            services.AddNCacheResponseCachingServices();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (!environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseResponseCaching();

            // response caching should be done only for GET requests
            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Method.Equals(HttpMethod.Get))
                {
                    ctx.Response.GetTypedHeaders().CacheControl =
                    new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(10)
                    };
                    ctx.Response.Headers[HeaderNames.Vary] =
                        new string[] { "Accept-Encoding" };
                }

                await next();
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                e.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
