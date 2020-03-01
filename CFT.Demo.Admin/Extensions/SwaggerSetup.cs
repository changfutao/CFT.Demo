using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CFT.Demo.Admin.Extensions
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //获取当前应用程序的根路径
            var basePath = AppContext.BaseDirectory;
            //注册一个或多个Swagger文档
            services.AddSwaggerGen(options =>
            {
                //添加Swagger文档
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    //版本号
                    Version = "1.0",
                    //标题
                    Title = "CFT.Demo.Admin",
                    //描述
                    Description = "ASP.NET Core 学习项目"
                    //作者联系
                    //Contact = new OpenApiContact
                    //{
                    //    Email = "981384763@qq.com",
                    //    Name = "CFT.Demo.Admin",
                    //    Url = new Uri("")
                    //},
                    //许可说明
                    //License = new OpenApiLicense { Name="CFT.Demo.Admin 官方文档", Url=new Uri("")}
                });
                //不知道作用
                options.OrderActionsBy(c => c.RelativePath);

                try
                {
                    //获取Controller的XML地址
                    string controllerXmlPath = basePath + "CFT.Demo.Admin.xml";
                    //默认第二个参数时false,这个是Controller注释,需要改成true
                    options.IncludeXmlComments(controllerXmlPath, true);

                    //获取Entity的XML地址
                    string entityXmlPath = basePath + "CFT.Demo.Entity.xml";
                    options.IncludeXmlComments(entityXmlPath);
                }
                catch (Exception ex)
                {
                   //记录日志
                }

                //开启加权小锁
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在header中添加token,传递到后台
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                //必须是oauth2
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme 
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
            return services;
        }
    }
}
