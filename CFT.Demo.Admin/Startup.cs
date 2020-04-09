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
            #region 依赖注入生命周期
            //AddTransient 暂时生存期服务 每次从服务容器进行请求时创建(每次创建都是新的)
            //AddScoped 每次Http请求创建一次(一次Http请求下,都是同一个)
            //AddSingleton 整个服务生存周期都是同一个
            //services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();

            //services.Add(new ServiceDescriptor(typeof(IOperationScoped), typeof(Operation), ServiceLifetime.Scoped));
            #endregion

            //Swagger
            services.AddSwaggerSetup();

            //添加验证器
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
                //模型验证
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

            #region 路由
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
            ////中间件:处理Http请求和响应的组件(多个中间件之间链式关系使之形成了管道)
            ////中间件顺序是按照注册顺序 1->2->3->2->1 
            ////每一个Use方法,都是在await next(); 前 后执行操作

            ////context HttpContext
            ////next 管道中的下一个委托
            //app.Use(async(context, next) => 
            //{
            //    Console.WriteLine("Middleware one start");
            //    //调用下一个中间件
            //    await next();
            //    Console.WriteLine("Middleware one end");
            //});

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware two start"); 
            //    //调用下一个中间件
            //    await next();
            //    Console.WriteLine("Middleware two end------");
            //});

            ////Map 扩展用作约定来创建管道分支
            ////Map 基于给定请求路径的匹配项来创建请求管道分支
            ////Map 会根据是否匹配指定的请求的路径是否在一个新的分支上继续执行后续的中间件,并且在新分支上执行完毕后,不再回到原来的管道上
            //app.Map(new PathString("/path1"), builder =>
            //{
            //    builder.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("我是path1分支 start");
            //        await next();
            //        Console.WriteLine("我是path1分支 end");
            //    });

            //    builder.Use(async (context, next) => 
            //    {
            //        Console.WriteLine("我是path2分支 start");
            //        await next();
            //        Console.WriteLine("我是path2分支 end");
            //    });
            //    builder.Run(async context =>
            //    {
            //        Console.WriteLine("map end");
            //        await context.Response.WriteAsync("我是map分支的结束");
            //    });
            //});

            //app.MapWhen(context =>
            //    context.Request.Path.Value == "/maptest"
            //, app => 
            //{
            //    app.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("中间件B 开始");
            //        await next();
            //        Console.WriteLine("中间件B 结束");
            //    });
            //}) ;

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware three start");
            //    //调用下一个中间件
            //    await next();
            //    Console.WriteLine("Middleware three end------");
            //});

            ////Run 终端中间件
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("request end");
            //});
            #endregion

            //注入静态文件中间件
            app.UseStaticFiles();

            #region Swagger
            //注册Swagger中间件
            app.UseSwagger();
            //注册SwaggerUI(图形界面)
            app.UseSwaggerUI(options =>
            {
                // /swagger/V1/swagger.json  V1 要与 SwaggerDoc的Name一致,否则找不到会报错
                options.SwaggerEndpoint("/swagger/V1/swagger.json", "ApiHelp V1");
                //路径配置，设置为空，表示直接访问该文件
                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，
                //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可
                options.RoutePrefix = "";
            });
            #endregion

            //开启身份验证
            app.UseAuthentication();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
