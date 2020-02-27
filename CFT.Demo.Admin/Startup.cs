using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CFT.Demo.Admin
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
            services.AddControllers();
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env
            )
        {
            #region IWebHostEnvironment
            //IWebHostEnvironment.ApplicationName Web应用程序名字
            Console.WriteLine($"当前Web应用程序的名字是:{env.ApplicationName}");
            //IWebHostEnvironment.ContentRootPath Web应用程序的绝对路径
            Console.WriteLine($"当前Web应用程序的绝对路径是:{env.ContentRootPath}");
            //IWebHostEnvironment.EnvironmentName 当前Web应用程序的环境
            Console.WriteLine($"当前应用程序的环境:{env.EnvironmentName}");
            //IWebHostEnvironment.WebRootPath 当前Web应用程序wwwroot的绝对目录
            Console.WriteLine($"当前应用程序的wwwroot文件夹的绝对目录:{env.WebRootPath}");
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //注入静态文件中间件
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
