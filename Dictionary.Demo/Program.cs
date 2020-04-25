using System;
using System.Collections.Generic;

namespace Dictionary.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary_Demo();
            Console.ReadKey();
        }

        private static void SortedDictionary_Demo()
        {
            //SortedDictionary<TKey,TValue> 类
            //表示根据键进行排序的键/值对的集合

            var data= new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
