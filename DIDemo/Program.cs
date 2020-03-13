using System;
using Microsoft.Extensions.DependencyInjection;


namespace DIDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingleton>(
                new Operation(Guid.Empty)
                );
            services.AddSingleton<IOperationSingleton>(
                new Operation(Guid.NewGuid())
                );

            services.AddScoped<IOperationScoped, Operation>();

            var provider = services.BuildServiceProvider();

            //输出singletone1的Guid
            var singletone1 = provider.GetService<IOperationSingleton>();
            Console.WriteLine($"signletone1: {singletone1.OperationId}");

            // 输出singletone2的Guid
            var singletone2 = provider.GetService<IOperationSingleton>();
            Console.WriteLine($"signletone2: {singletone2.OperationId}");
            Console.WriteLine($"singletone1 == singletone2 ? : { singletone1 == singletone2 }");

            //输出scope的Guid
            var scope1 = provider.GetService<IOperationScoped>();
            Console.WriteLine($"scope1: {scope1.OperationId}");

            var scope2 = provider.GetService<IOperationScoped>();
            Console.WriteLine($"scope2: {scope2.OperationId}");
            //CreateScope会产生一个新的ServiceProvider范围,在这个范围下的Scope标注的实例将只会是同一个实例
            //用Scope注册的对象，在同一个ServiceProvider的 Scope下相当于单例
            using (var scope = provider.CreateScope())
            {
                var p = scope.ServiceProvider;
                var scope3 = p.GetService<IOperationScoped>();
                Console.WriteLine($"scope3: {scope3.OperationId}");
            }
            Console.ReadKey();
        }
    }
}
