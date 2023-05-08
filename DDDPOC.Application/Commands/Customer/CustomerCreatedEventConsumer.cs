using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Application.Commands.Customer
{
    public sealed class CustomerCreatedEventConsumer : IConsumer<CreateCustomerEvent>
    {
        private readonly ILogger<CustomerCreatedEventConsumer> _logger;

        public CustomerCreatedEventConsumer(ILogger<CustomerCreatedEventConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<CreateCustomerEvent> context)
        {
            _logger.LogInformation("Customer Created: {@Customer}", context.Message);
            return Task.CompletedTask;
        }
    }
}
