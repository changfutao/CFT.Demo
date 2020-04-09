using CFT.Demo.Admin.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.ValidateAttributes
{
    public class ActionParameterValidationFilterAttribute:ActionFilterAttribute
    {
        private IOperationScoped _operationScoped;
        public ActionParameterValidationFilterAttribute(IOperationScoped operationScoped)
        {
            _operationScoped = operationScoped;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var request= context.HttpContext.Request;
            if(request.Method.ToLower() =="get")
            {
                Console.WriteLine("get"+_operationScoped.OperationId);
            }
            base.OnActionExecuted(context);
        }
    }
}
