using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeguridadEmpleados.Data;
using SeguridadEmpleados.Repositories;

namespace MvcCore
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme =
                    CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                    CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme =
                    CookieAuthenticationDefaults.AuthenticationScheme;
                }).AddCookie();
            services.AddTransient<RepositoryEmpleados>();
            services.AddDbContext<EmpleadosContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("cadenahospital")));
            services.AddControllersWithViews
                (option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default"
                    , template: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}"
            //    );
            //});
        }
    }
}
