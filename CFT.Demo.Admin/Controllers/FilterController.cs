using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.ValidateAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFT.Demo.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        //[ActionParameterValidationFilter]
        [TypeFilter(typeof(ActionParameterValidationFilterAttribute))]
        [Route("Filter1")]
        [HttpGet]
        public string Filter1()
        {
            return "我是Filter1方法";
        }
    }
}