using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CFT.Demo.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private ILogger<LogController> _logger;
        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 基本使用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BaseUse()
        {
            //_logger.Log(LogLevel.Information, 0, "进入LogController");
            _logger.LogInformation("进入LogController");
            return Ok();
        }
        /// <summary>
        /// 分组使用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ScopeUse()
        {
            using(_logger.BeginScope("获取数据"))
            {
                _logger.LogInformation("准备获取数据");
                if(true)
                {
                    _logger.LogError("数据不存在");
                }
                return Ok();
            }
        }
    }
}