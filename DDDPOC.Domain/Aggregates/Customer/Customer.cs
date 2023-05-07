using Ardalis.GuardClauses;
using DDDPOC.SharedKernel.Interfaces;

namespace DDDPOC.Domain.Aggregates
{
    public class Customer : AggregateRoot<Guid>
    {
        private readonly List<Order> orders;
        public string CustomerName { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }

        private Customer()
        {

        }
        private Customer(string customerName, string address, string email) : base()
        {
            CustomerName = Guard.Against.NullOrEmpty(customerName, nameof(customerName));
            Address = Guard.Against.NullOrEmpty(customerName, nameof(address));
            Email = Guard.Against.NullOrEmpty(customerName, nameof(email));
            orders = new List<Order>();
        }
        public IReadOnlyList<Order> Orders => orders;

        public static Customer Create(string customerName, string address, string email)
        {

            var customer = new Customer(customerName, address, email);
            customer.RaiseDomainEvent(new CustomerAddedEvent("add new customer"));
            return customer;
        }

    }
}
