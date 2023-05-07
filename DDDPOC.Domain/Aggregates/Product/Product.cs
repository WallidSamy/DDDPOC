using Ardalis.GuardClauses;
using DDDPOC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Domain.Aggregates
{
    public class Product : BaseEntity<Guid>
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }


        public Product(string productName, double price, string description)
        {
            ProductName = productName;
            Price = Guard.Against.NegativeOrZero(price, nameof(price), "Negative Or Zero");
            Description = description;
        }

    }
}
