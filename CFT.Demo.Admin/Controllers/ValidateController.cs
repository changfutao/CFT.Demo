using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.ValidateModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFT.Demo.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateController : ControllerBase
    {
        /*
            针对Post
                 
        */
        [HttpPost(template:"Login")]
        public IActionResult Login([FromBody]User user)
        {

            return null;
        }
    }
}