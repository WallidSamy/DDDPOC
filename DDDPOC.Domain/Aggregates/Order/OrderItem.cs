using DDDPOC.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDDPOC.Domain.Aggregates
{
    public class OrderItems : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public OrderItems(int quantity, Guid productId)
        {
            Quantity = quantity;
            ProductId = productId;
        }
    }
}
