using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Models
{
    public class Test : ITest
    {
        public Test()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; }

        public string Name { get; set; }
    }
}
