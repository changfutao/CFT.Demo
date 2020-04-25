using Microsoft.Extensions.Configuration;
using System;
using System.IO;


namespace ConfigureDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region 读取Json数据
            Console.WriteLine("工作目录:" + Directory.GetCurrentDirectory());
            var builder = new ConfigurationBuilder()
                          //设置工作目录
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("config.json");

            var config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine($"Key:{item.Key},Value:{item.Value}");

                //通过指定Key来访问其配置项值
                Console.WriteLine($"FontFamily:{config["FontFamily"]}");
            }

            var section = config.GetSection("Editor");
            Console.WriteLine($"section:{section["Background"]}");
            Console.WriteLine($"section:{section["Foreground"]}");
            #endregion
            Console.WriteLine("===========================================");

            #region 读取XML数据
            var buildxml = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("appconfig.xml");
            var configXml = buildxml.Build();
            foreach (var item in configXml.AsEnumerable())
            {
                Console.WriteLine($"Key:{item.Key},Value:{item.Value}");
            }

            //var xmlName = configXml["AppInfo:Name"];
            //var xmlVersion = configXml["AppInfo:Version"];

            var xmlName = configXml["Name"];
            #endregion

            var a = 1;
            Console.ReadKey();
        }
    }
}