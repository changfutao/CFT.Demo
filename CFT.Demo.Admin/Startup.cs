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
using CFT.Demo.Admin.Extensions;
using Microsoft.AspNetCore.Routing;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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
            #region ����ע����������
            //AddTransient ��ʱ�����ڷ��� ÿ�δӷ���������������ʱ����(ÿ�δ��������µ�)
            //AddScoped ÿ��Http���󴴽�һ��(һ��Http������,����ͬһ��)
            //AddSingleton ���������������ڶ���ͬһ��
            //services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();

            //services.Add(new ServiceDescriptor(typeof(IOperationScoped), typeof(Operation), ServiceLifetime.Scoped));
            #endregion

            //Swagger
            services.AddSwaggerSetup();

            //�����֤��
            services.AddSingleton<IValidator<UserDto>, UserValidator>();
            //
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<ActionParameterValidationFilter>();
            })
                //ģ����֤
                .AddFluentValidation();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                  {
                      var errors = context.ModelState
                                         .Values
                                         .SelectMany(x => x.Errors
                                         .Select(p => p.ErrorMessage))
                                         .ToList();

                      var result = new
                      {
                          Success = false,
                          Message = "Validation errors",
                          Error = errors
                      };
                      return new BadRequestObjectResult(result);
                  };
            });
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

            #region ·��
            //var trackPackageRouteHandler = new RouteHandler(context =>
            // {
            //    var routeValues = context.GetRouteData().Values;
            //     return context.Response.WriteAsync($"Hello! Route values:{string.Join(",",routeValues)}");
            // });

            // var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);

            // routeBuilder.MapRoute("Track Package Route", "package/{operation}/{id:int}");

            // routeBuilder.MapGet("hello/{name}", context =>
            //  {
            //      var name = context.GetRouteValue("name");
            //      return context.Response.WriteAsync($"Hi, {name}!");
            //  });

            // var routes = routeBuilder.Build();
            // app.UseRouter(routes);
            #endregion

            #region Middlewares
            ////�м��:����Http�������Ӧ�����(����м��֮����ʽ��ϵʹ֮�γ��˹ܵ�)
            ////�м��˳���ǰ���ע��˳�� 1->2->3->2->1 
            ////ÿһ��Use����,������await next(); ǰ ��ִ�в���

            ////context HttpContext
            ////next �ܵ��е���һ��ί��
            //app.Use(async(context, next) => 
            //{
            //    Console.WriteLine("Middleware one start");
            //    //������һ���м��
            //    await next();
            //    Console.WriteLine("Middleware one end");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware two start"); 
            //    //������һ���м��
            //    await next();
            //    Console.WriteLine("Middleware two end------");
            //});

            ////Map ��չ����Լ���������ܵ���֧
            ////Map ���ڸ�������·����ƥ��������������ܵ���֧
            ////Map ������Ƿ�ƥ��ָ���������·���Ƿ���һ���µķ�֧�ϼ���ִ�к������м��,�������·�֧��ִ����Ϻ�,���ٻص�ԭ���Ĺܵ���
            //app.Map(new PathString("/path1"), builder =>
            //{
            //    builder.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("����path1��֧ start");
            //        await next();
            //        Console.WriteLine("����path1��֧ end");
            //    });

            //    builder.Use(async (context, next) => 
            //    {
            //        Console.WriteLine("����path2��֧ start");
            //        await next();
            //        Console.WriteLine("����path2��֧ end");
            //    });
            //    builder.Run(async context =>
            //    {
            //        Console.WriteLine("map end");
            //        await context.Response.WriteAsync("����map��֧�Ľ���");
            //    });
            //});

            //app.MapWhen(context =>
            //    context.Request.Path.Value == "/maptest"
            //, app => 
            //{
            //    app.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("�м��B ��ʼ");
            //        await next();
            //        Console.WriteLine("�м��B ����");
            //    });
            //}) ;

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware three start");
            //    //������һ���м��
            //    await next();
            //    Console.WriteLine("Middleware three end------");
            //});

            ////Run �ն��м��
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("request end");
            //});
            #endregion

            //ע�뾲̬�ļ��м��
            app.UseStaticFiles();

            #region Swagger
            //ע��Swagger�м��
            app.UseSwagger();
            //ע��SwaggerUI(ͼ�ν���)
            app.UseSwaggerUI(options =>
            {
                // /swagger/V1/swagger.json  V1 Ҫ�� SwaggerDoc��Nameһ��,�����Ҳ����ᱨ��
                options.SwaggerEndpoint("/swagger/V1/swagger.json", "ApiHelp V1");
                //·�����ã�����Ϊ�գ���ʾֱ�ӷ��ʸ��ļ�
                //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�
                //���ʱ��ȥlaunchSettings.json�а�"launchUrl": "swagger/index.html"ȥ���� Ȼ��ֱ�ӷ���localhost:8001/index.html����
                options.RoutePrefix = "";
            });
            #endregion

            //���������֤
            app.UseAuthentication();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
