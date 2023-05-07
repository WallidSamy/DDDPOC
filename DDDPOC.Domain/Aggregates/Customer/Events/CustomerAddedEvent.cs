using DDDPOC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Domain.Aggregates
{
    public class CustomerAddedEvent : BaseDomainEvent
    {
        public CustomerAddedEvent(string message)
        {
            Message = message;
        }
    }
}
