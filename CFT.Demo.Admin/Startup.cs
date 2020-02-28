using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.Filters;
using CFT.Demo.Admin.Models;
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
            //services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
            services.AddTransient<ITest, Test>();
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

            #region Middlewares
            //中间件顺序是按照注册顺序 1->2->3->2->1 
            //每一个Use方法,都是在await next(); 前 后执行操作

            //context HttpContext
            //next 管道中的下一个委托
            app.Use(async(context, next) => 
            {
                Console.WriteLine("Middleware one start");
                //调用下一个中间件
                await next();
                Console.WriteLine("Middleware one end");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Middleware two start");
                //调用下一个中间件
                await next();
                Console.WriteLine("Middleware two end");
            });

            //Map 扩展用作约定来创建管道分支
            //Map 基于给定请求路径的匹配项来创建请求管道分支
            app.Map("/path1", builder => 
            {
                builder.Use(async (context, next) => 
                {
                    Console.WriteLine("我是path1分支 start");
                    await next();
                    Console.WriteLine("我是path1分支 end");
                });
            });

            //Run 终端中间件
            app.Run(async context =>
            {
                await context.Response.WriteAsync("request end");
            });
            #endregion


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
