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
            //nlog.config��Ĭ�ϵ�����(��������������ʡ���������)
            NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                //��ȡ��ǰӦ�ó���·��
                var path = hostingContext.HostingEnvironment.ContentRootPath+"\\Configs\\questUrl.json";
                //path: �ļ���·��  optional: �Ƿ��Ǳ����  reloadOnChange: ���json�Ķ�������������
                config.AddJsonFile(path:path,optional:false,reloadOnChange:true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                #region ����ֱ����Program.cs�ļ���ֱ������ע�������м��(������StartUp��)
                //webBuilder.ConfigureServices(services =>
                //     {
                //    //ע��WebApi
                //    services.AddControllers();
                //     });
                //webBuilder.Configure(builder =>
                //{
                //    builder.UseAuthentication();
                //}); 
                #endregion
                webBuilder.UseStartup<Startup>()
                          .ConfigureLogging(logging => 
                          {
                              //�Ƴ��Ѿ�ע���������־�������
                              logging.ClearProviders();
                              //������С����־����
                              logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                          })
                          .UseNLog();
            });
    }
}