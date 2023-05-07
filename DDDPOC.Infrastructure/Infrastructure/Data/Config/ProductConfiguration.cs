using DDDPOC.Domain.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Infrastructure.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd()
                                        .HasValueGenerator<GuidValueGenerator>();
            builder.Property(e => e.ProductName).IsRequired();
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.Description).IsRequired();

        }
    }
}
