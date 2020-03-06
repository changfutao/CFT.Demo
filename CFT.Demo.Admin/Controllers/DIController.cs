using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFT.Demo.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DIController : ControllerBase
    {
        private readonly ITest _test;
        private readonly ITest _test1;
        private readonly ITest _test2;
        public DIController(
            ITest test,
            ITest test1,
            ITest test2
            )
        {
            this._test = test;
            this._test1 = test1;
            this._test2 = test2;
        }
        [HttpGet]
        public string GetString()
        {
            string guid1 = _test.Guid.ToString("d"); //Transient 
            //string guid2 =
            return _test.Guid.ToString("d");
        }
    }
}