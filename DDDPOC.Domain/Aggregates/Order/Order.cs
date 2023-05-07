using DDDPOC.SharedKernel;

namespace DDDPOC.Domain.Aggregates
{
    public class Order : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public ICollection<OrderItems> orderItems { get; set; } = new List<OrderItems>();

    }
}
