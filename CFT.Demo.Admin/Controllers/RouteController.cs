using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFT.Demo.Admin.Controllers
{
    /*
        注意: 基于约定的路由与特性路由方式可以同时存在，但是如果已经为一个Action指定了特性路由，那么基于约定的路由在该Action就不会起作用了
    */
    //访问http://localhost:5000/api/Route
    //[Route("api/[controller]")]
    //对于web api应用程序而言,URL作为接口应该尽量避免变动,因此仍建议写成固定值
    [Route("api/Route")]
    public class RouteController : ControllerBase
    {
        //都可以访问到Method1方法 特性路由更加灵活
        ////访问http://localhost:5000 
        //[Route("")]
        ////访问http://localhost:5000/Route1/Method
        //[Route("Route1/Method")]
        ////访问http://localhost:5000/Method1
        //[Route("Method1")]
        //[HttpGet]
        //public string Method1()
        //{
        //    return "我是Method1";
        //}

        [Route("Method2")]
        [HttpGet]
        public string Method2()
        {
            return "我是Method2";
        }
        [Route("Method33/{parameter1}")]
        [HttpGet]
        public string Method3(string parameter1)
        {
            return "我是Method3,参数是" + parameter1;
        }
        [Route("Method4/{name?}")]
        [HttpGet()]
        public string Method4(string name)
        {
            string msg = "";
            if(!string.IsNullOrWhiteSpace(name))
            {
                msg = name;
            }
            return "我是Method4,参数是"+msg;
        }
        //HttpGet 给template赋值的话,不能加Route
        [HttpGet("{msg}")]
        public string Method5(string msg)
        {
            return "我是Method5,参数是"+msg;
        }
        [HttpGet("Method6/{tt}")]
        public string Method6(string tt)
        {
            return "我是Method6";
        }
    }
}