using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Viivalista
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(1); });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Kirjautunut", policy => policy.RequireClaim(ClaimTypes.PrimarySid));
                options.AddPolicy("Esimies", policy => policy.RequireRole("True"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AccessDeniedPath = "/",
                AuthenticationScheme = "MyCookieAuthenticationScheme",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = "/Home/Kirjautuminen/"
            });




            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "Tyontekija",
                    template: "{controller=Tyontekija}/{action=index}/{id?}");
            });
        }
    }
}
