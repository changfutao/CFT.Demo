using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CFT.Demo.Admin.Controllers
{
    [Route("api/[controller]")]
    public class ConfigureController : Controller
    {
        private IConfiguration _config;
        private ILogger<ConfigureController> _logger;
        public ConfigureController(
            IConfiguration config,
            ILogger<ConfigureController> logger
            )
        {
            _config = config;
            _logger = logger;
        }
        [Route("GetJsonFile")]
        [HttpGet]
        public string GetJsonFile()
        {
            /*
             * 注意: 外部json和appsettings.json 都可以用IConfiguration访问
            */
            //1.获取Json文件的 key:value  
            string str = _config["testWebUrl"];
            
            //2.获取Json文件的 key:{}
            var testWeb = new TestWeb();
            _config.GetSection("testB").Bind(testWeb);

            //3.获取application.json的数据
            string apiName = _config["APIName"];

            return string.Empty;
        }
        [Route("GetStr")]
        [HttpGet]
        public string GetStr(string option)
        {
            _logger.LogInformation(option);
            return "";
        }
    }
}
