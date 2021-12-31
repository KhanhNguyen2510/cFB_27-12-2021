using cFB.IntergrationAPI.Historys;
using cFB.IntergrationAPI.Posts;
using cFB.IntergrationAPI.Reports;
using cFB.IntergrationAPI.Systems.Users;
using cFB.IntergrationAPI.WatchLists;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace cFB.Wedsite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Index";
                    options.AccessDeniedPath = "/User/Forbidden/";
                });

            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddTransient<IWatchListApiClient, WatchListApiClient>();
            services.AddTransient<IPostApiClient, PostApiClient>();
            services.AddTransient<IHistoryApiClient, HistoryApiClient>();
            services.AddTransient<IReportApiClient, ReportApiClient>();
            services.AddTransient<IUserApiSevice, UserApiClient>();


            IMvcBuilder builder = services.AddRazorPages();

            var envirament = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (envirament == Environments.Development)
            {
                builder.AddRazorRuntimeCompilation();

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }
            
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
