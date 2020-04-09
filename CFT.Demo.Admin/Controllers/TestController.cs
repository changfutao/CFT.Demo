using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFT.Demo.Admin.Controllers
{
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test1()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult Test2()
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        [Route("Test3")]
        [HttpGet]
        public IActionResult Test3()
        {
            var result = new OkObjectResult(new { message = "操作成功", currentDate = DateTime.Now });
            return result;
        }
        [Route("Test4")]
        [HttpGet]
        public string Test4([FromHeader] string clientId)
        {
            return $"ClientID 是 {clientId}";
        }
    }
}