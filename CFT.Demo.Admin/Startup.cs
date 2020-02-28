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
            //IWebHostEnvironment.ApplicationName WebӦ�ó�������
            Console.WriteLine($"��ǰWebӦ�ó����������:{env.ApplicationName}");
            //IWebHostEnvironment.ContentRootPath WebӦ�ó���ľ���·��
            Console.WriteLine($"��ǰWebӦ�ó���ľ���·����:{env.ContentRootPath}");
            //IWebHostEnvironment.EnvironmentName ��ǰWebӦ�ó���Ļ���
            Console.WriteLine($"��ǰӦ�ó���Ļ���:{env.EnvironmentName}");
            //IWebHostEnvironment.WebRootPath ��ǰWebӦ�ó���wwwroot�ľ���Ŀ¼
            Console.WriteLine($"��ǰӦ�ó����wwwroot�ļ��еľ���Ŀ¼:{env.WebRootPath}");
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Middlewares
            //�м��˳���ǰ���ע��˳�� 1->2->3->2->1 
            //ÿһ��Use����,������await next(); ǰ ��ִ�в���

            //context HttpContext
            //next �ܵ��е���һ��ί��
            app.Use(async(context, next) => 
            {
                Console.WriteLine("Middleware one start");
                //������һ���м��
                await next();
                Console.WriteLine("Middleware one end");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Middleware two start");
                //������һ���м��
                await next();
                Console.WriteLine("Middleware two end");
            });

            //Map ��չ����Լ���������ܵ���֧
            //Map ���ڸ�������·����ƥ��������������ܵ���֧
            app.Map("/path1", builder => 
            {
                builder.Use(async (context, next) => 
                {
                    Console.WriteLine("����path1��֧ start");
                    await next();
                    Console.WriteLine("����path1��֧ end");
                });
            });

            //Run �ն��м��
            app.Run(async context =>
            {
                await context.Response.WriteAsync("request end");
            });
            #endregion


            //ע�뾲̬�ļ��м��
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
