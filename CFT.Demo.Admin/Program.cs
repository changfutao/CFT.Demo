using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using NLog.Web;

namespace CFT.Demo.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //nlog.config是默认的名字(如果名字是这个可省略下面代码)
            NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //为应用程序添加配置
            .ConfigureAppConfiguration((hostingContext, config) =>  //读取配置
            {
                //获取当前应用程序路径
                var path = hostingContext.HostingEnvironment.ContentRootPath+"\\Configs\\questUrl.json";
                //path: 文件的路径  optional: 是否是必须的  reloadOnChange: 如果json改动，会立即更新
                config.AddJsonFile(path:path,optional:false,reloadOnChange:true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                #region 可以直接在Program.cs文件中直接依赖注入和添加中间件(不适用StartUp类)
                //webBuilder.ConfigureServices(services =>
                //     {
                //    //注入WebApi
                //    services.AddControllers();
                //     });
                //webBuilder.Configure(builder =>
                //{
                //    builder.UseAuthentication();
                //}); 
                #endregion
                webBuilder.UseStartup<Startup>()
                          //配置Kestrel服务器作为默认的Web服务器来负责处理Web请求与响应
                          .UseKestrel(options => //用于指定服务器使用Kestrel 
                          {
                              //限制请求正文的最大值
                              options.Limits.MaxRequestBodySize = 10 * 1024;
                          })  
                          //.UseContentRoot() 为应用程序指定根目录 需注意这和StaticFiles的根是不同的,虽然默认情况下StaticFiles的根是以ContentRoot为依据([ContentRoot/wwwroot])
                          .ConfigureLogging(logging => //配置日志处理程序
                          {
                              //移除已经注册的其他日志处理程序
                              logging.ClearProviders();
                              //设置最小的日志级别
                              logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                          })
                          .UseNLog();
            });
    }
}