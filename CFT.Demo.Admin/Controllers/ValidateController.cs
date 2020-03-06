using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.ValidateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFT.Demo.Admin.Controllers
{
    [Authorize("RequireAdministratorRole")]
    [Produces("application/json")]  //声明控制器的操作支持 application/json 的响应内容类型 
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateController : ControllerBase
    {
        /*
            针对Post方法,如果请求没有Body,参数user 就是null;如果body里面的数据所包含的属性在product中不存在,那么这个属性就会被忽略
            如果body数据的属性有问题,比如username 没有填写,那么在执行action方法的时候就会报错,会响应500
            
            415状态码 服务器拒绝服务，原因是请求格式不被支持
        */
        /// <summary>
        /// 登录
        /// </summary>
        /// Sample request
        /// POST /Todo
        /// {
        ///     UserName:"admin",
        ///     PassWord:"admin"
        /// }
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="400">If the item is null</response>
        [HttpPost(template:"Login")]
        public IActionResult Login([FromBody]User user)
        {
            if(user == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return new JsonResult(new { });
        }
    }
}