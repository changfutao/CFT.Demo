using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFT.Demo.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CFT.Demo.Admin.Controllers
{
    /*
        1.依赖
        当一个类需要另一个类协作来完成工作的时候就产生了依赖 
        设计原则: 依赖于抽象,而不是具体的实现

        2.注入
        注入体现的是一个IOC(控制反转的思想)
        把依赖的创建丢给其它人,自己只负责使用,其它人丢给你依赖的这个过程理解为注入

        3.反转
        为了在业务变化的时候尽少改动代码可能造成的问题(比如原来用EF去验证登录改为用Redis去读)
        AccountController 依赖于 ILoginService
        EFLoginService:ILoginService
        RedisLoginService:ILoginService

        4.容器
        管理所有的依赖
            4.1 绑定服务与实例之间的关系
            4.2 获取实例,并对实例进行管理(创建与销毁)

        5.实例的注册
            5.1 IServiceCollection 负责注册
            5.2 IServiceProvider 负责提供实例

        6.实例的生命周期之单例
            6.1 Transient: 每一次GetService都会创建一个新的实例
            6.2 Scoped: 在同一个Scope内只初始化一个实例,可以理解为(每一个request级别只创建一个实例,同一个http request会在一个scope内)
            6.3 Singleton: 整个应用程序生命周期内只创建一个实例
    */
    [Route("api/[controller]")]
    [ApiController]
    public class DIController : ControllerBase
    {
        private readonly IOperationTransient _operation1;
        private readonly IOperationScoped _operation2;
        private readonly IOperationSingleton _operation3;
        public DIController(
            IOperationTransient operation1,
            IOperationScoped operation2,
            IOperationSingleton operation3
            )
        {
            _operation1 = operation1;
            _operation2 = operation2;
            _operation3 = operation3;
        }
        [HttpGet]
        public string GetString()
        {
            string guid1_1 = _operation1.OperationId.ToString("d");
            string guid1_2 = HttpContext.RequestServices.GetService<IOperationTransient>().OperationId.ToString("d");
            string guid2_1 = _operation2.OperationId.ToString("d");
            string guid2_2 = HttpContext.RequestServices.GetService<IOperationScoped>().OperationId.ToString("d");
            string guid3 = _operation3.OperationId.ToString("d");

            return $"Transient:{guid1_1}\r\nTransient:{guid1_2}\r\nScoped:{guid2_1}\r\nScoped:{guid2_2}\r\nSingleton:{guid3}";
        }
    }
}