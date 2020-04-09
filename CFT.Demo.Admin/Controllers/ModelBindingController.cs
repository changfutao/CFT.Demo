using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.Models;
using CFT.Demo.Admin.ValidateAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFT.Demo.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelBindingController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody]BlogDto blogDto)
        {
            return Ok();
        }
        [Route("Post1")]
        [HttpPost]
        public IActionResult Post1([FromBody]BlogDto blogDto)
        {
            return new JsonResult(new { status=200});
        }
        [Route("Post2")]
        [HttpPost]
        public IActionResult Post2([FromBody]UserDto userDto)
        {
            return Ok();
        }
        [Route("Post3")]
        [HttpGet]
        public IActionResult Post3([FromQuery]string msg)
        {
            return Ok();
        }
    }
}