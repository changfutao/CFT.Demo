﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CFT.Demo.Admin.Controllers
{
    [Route("api/[controller]")]
    public class ConfigureController : Controller
    {
        private IConfiguration _config;
        private ILogger<ConfigureController> _logger;
        private UISetting _uiSetting;
        private UISetting _uiSettingSingleton;
        public ConfigureController(
            IConfiguration config,
            ILogger<ConfigureController> logger,
            IOptionsSnapshot<UISetting> uiSettingOptions,
            UISetting uiSettingSingleton
            )
        {
            _config = config;
            _logger = logger;
            _uiSetting = uiSettingOptions.Value;
            _uiSettingSingleton = uiSettingSingleton;
        }
        /// <summary>
        /// 获取Json文件
        /// </summary>
        /// <returns></returns>
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
        [Route("IOCTest")]
        [HttpGet]
        public string IOCTest()
        {
            var test = HttpContext.RequestServices.GetRequiredService<IOperationScoped>();
            return "";
        }
        [HttpGet(template: "Test1")]
        public string Test()
        {
            return "test";
        }

        #region 读取分层配置数据
        [HttpGet("ReadSettingData")]
        public string ReadSettingData()
        {
            //读取json文件时,使用冒号(:)
            string cnblogs = _config["ExtUrl:A"];
            return cnblogs;
        }
        #endregion
        [Route("ReadAppSettings")]
        [HttpGet]
        public string ReadAppSettings()
        {
            string version = _config["Version"];
            UISetting setting = _uiSetting;
            return version;
        }
        [Route("BindModel")]
        [HttpGet]
        public string BindModel()
        {
            string aa = "";
            return aa;
        }

    }
}
