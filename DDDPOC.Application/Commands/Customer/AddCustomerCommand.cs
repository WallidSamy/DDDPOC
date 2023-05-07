using DDDPOC.Infrastructure.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;
using DDDPOC.Domain.Aggregates;


namespace DDDPOC.Application.Commands
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

        public AddCustomerHandler(IRepository<Customer, Guid> customerRepo,
                                IUnitOfWork unitOfWork)
        {
            _customerRepo = customerRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = Customer.Create(request.CustomerName, request.Address, request.Email);
                _customerRepo.Add(customer);
                _unitOfWork.SaveChanges();
                await _customerRepo.RaisEvents(customer);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
