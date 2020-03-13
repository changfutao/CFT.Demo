using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Models
{
    public class Operation : IOperationTransient,
        IOperationScoped,
        IOperationSingleton
    {
        public Operation() : this(Guid.NewGuid())
        {
        }

        public Operation(Guid id)
        {
            OperationId = id;
        }

        public Guid OperationId { get; private set; }
    }
}
