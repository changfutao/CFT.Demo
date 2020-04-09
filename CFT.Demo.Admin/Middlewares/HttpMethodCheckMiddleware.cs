using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Middlewares
{
    public class HttpMethodCheckMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpMethodCheckMiddleware(
            RequestDelegate next,
            IHostingEnvironment environment)
        {
            this._next = next;
        }

        //public Task Invoke(HttpContext context)
        //{

        //}
    }
}
