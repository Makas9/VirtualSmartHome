using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SmartHome.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Hangfire;
using Hangfire.MemoryStorage;
using SmartHome.Device.Controllers;
using SmartHome.Areas.Device.Controllers;

namespace SmartHome
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
            services.AddDbContextPool<SmartHomeDbContext>(options => options.UseMySql(Configuration.GetConnectionString("SmartHomeDBConnection")));

            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            services.AddControllersWithViews();

            services.AddTransient<IScenarioRunService, ScenarioRunService>();

            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);//You can set Time   
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                name: "MyAreaResident",
                areaName: "Resident",
                pattern: "Resident/{controller=User}/{action=UserLogin}/{id?}");

                endpoints.MapAreaControllerRoute(
                name: "MyAreaRoom",
                areaName: "Room",
                pattern: "Room/{controller=Room}/{action=RoomList}/{id?}");

                endpoints.MapAreaControllerRoute(
                name: "MyAreaDevice",
                areaName: "Device",
                pattern: "Device/{controller=Device}/{action=OpenRoomDeviceList}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Resident}/{controller=User}/{action=UserLogin}/{id?}");
            });

            app.UseHangfireDashboard();

            recurringJobManager.AddOrUpdate(
                "Iterates through scenarios every minute",
                () => serviceProvider.GetService<IScenarioRunService>().IterateScenarios(),
                "* * * * *"
                );
        }
    }
}
