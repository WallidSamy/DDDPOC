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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>();
            builder.Property(e => e.CustomerName).IsRequired();
            builder.Property(e => e.Address).IsRequired();
            builder.Property(e => e.Email).IsRequired();
        }
    }
}
