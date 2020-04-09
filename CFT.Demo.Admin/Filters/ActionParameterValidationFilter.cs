using CFT.Demo.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Filters
{
    public class ActionParameterValidationFilter : IAsyncActionFilter
    {
        private IOperationScoped _operationScoped;
        public ActionParameterValidationFilter(IOperationScoped operationScoped)
        {
            _operationScoped = operationScoped;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            foreach (var parameter in descriptor.Parameters)
            {
                if(parameter.ParameterType == typeof(string))
                {
                    var argument = context.ActionArguments[parameter.Name];
                    if(argument.ToString().Contains(" "))
                    {
                        context.HttpContext.Response.Headers["X-ParameterValidation"] = "Fail";
                        //context.Result = new BadRequestObjectResult($"不能包含空格:{parameter.Name}");
                        context.Result = new BadRequestObjectResult(new { success = false, error = $"{parameter.Name}不能包含空格,{_operationScoped.OperationId}" });
                        break;
                    }
                }
            }

            if(context.Result == null)
            {
                var resultContext = await next();
                context.HttpContext.Response.Headers["X-ParameterValidation"] = "Success";

            }
        }
    }
}
