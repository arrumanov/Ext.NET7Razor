using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ext.Net;
using Ext.Net.Core;
using Westwind.AspNetCore.LiveReload;

namespace Company.WebApplication1
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
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // See https://github.com/RickStrahl/Westwind.AspnetCore.LiveReload
            services.AddLiveReload();

            // 1. Register Ext.NET services
            services.AddExtNet();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseLiveReload();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            // 2. Use Ext.NET resources
            //    To be added prior to app.UseStaticFiles()
            app.UseExtNetResources(config =>
            {
                config.UseEmbedded();
                config.UseThemeTriton();
            });

            // 3. Enable Ext.NET localization [not required]
            //    If included, localization will be handled automatically
            //    based on client browser preferences
            app.UseExtNetLocalization();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // 4. Ext.NET middleware
            //    To be added prior to app.UseEndpoints()
            app.UseExtNet(config =>
            {
                config.Theme = ThemeKind.Triton;
            });

            app.UseEndpoints(endpoints => endpoints.MapRazorPages());
        }
    }
}
