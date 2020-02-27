using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Middlewares
{
    public class RequestSetOptionsMiddleware
    {
        //每个Middleware都要声明一个RequestDelegate,目的?
        private readonly RequestDelegate _next;
        public RequestSetOptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// Invoke 名字是固定的写法
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var option = httpContext.Request.Query["option"];
            await _next(httpContext);
        }
    }
}
