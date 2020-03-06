using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Models
{
    public interface ITest
    {
        Guid Guid { get; }
        string Name { get; set; }
    }

    public interface ITest1
    {
        Guid Guid { get; }
        string Name { get; set; }
    }
}
