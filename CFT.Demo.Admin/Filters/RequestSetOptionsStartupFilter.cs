using CFT.Demo.Admin.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Filters
{
    /// <summary>
    /// IStartupFilter 由 ASP.NET Core 用于将默认值添加到管道的开头
    /// </summary>
    public class RequestSetOptionsStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            //return new Action<IApplicationBuilder>(builder => 
            //{
            //    builder.UseMiddleware<RequestSetOptionsMiddleware>();
            //    next(builder);
            //});

            //new Action<IApplicationBuilder>(app => { }); 相当于 app =>{ };
            return builder =>
            {
                builder.UseMiddleware<RequestSetOptionsMiddleware>();
                next(builder);
            };
        }

    }
}
