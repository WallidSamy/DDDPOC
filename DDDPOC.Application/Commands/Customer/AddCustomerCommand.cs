using DDDPOC.Application.Commands.Customer;
using DDDPOC.Domain.Aggregates;
using DDDPOC.Infrastructure.EventBus;
using DDDPOC.Infrastructure.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace DDDPOC.Application
{
    public class AddCustomerCommand : IRequest<bool>
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
    }

    public class AddCustomerHandler : IRequestHandler<AddCustomerCommand, bool>
    {
        private readonly IRepository<Customer, Guid> _customerRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;


        public AddCustomerHandler(IRepository<Customer, Guid> customerRepo,
                                IUnitOfWork unitOfWork,
                                IEventBus eventBus)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = Customer.Create(request.CustomerName, request.Address, request.Email);
                _customerRepo.Add(customer);
                _unitOfWork.SaveChanges();
                await _customerRepo.RaisEvents(customer);
                await _eventBus.PublishAsync(new CreateCustomerEvent()
                {
                    Id = customer.Id,
                    CustomerName = customer.CustomerName,
                    Address = customer.Address,
                    Email = customer.Email,
                },cancellationToken);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
