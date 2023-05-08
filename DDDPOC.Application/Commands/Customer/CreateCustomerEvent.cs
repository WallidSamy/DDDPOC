using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.Commands.Customer
{
    public record CreateCustomerEvent
    {
        public Guid Id { get; init; }
        public string CustomerName { get; init; }
        public string Address { get; init; }
        public string Email { get; init; }
    }
}
