using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Core.Entities;

namespace Shop.Data.Configrations
{
    internal class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.SalePrice).HasColumnType("money");
            builder.Property(x => x.CostPrice).HasColumnType("money");
            builder.Property(x => x.DiscountPercent).HasColumnType("money");
          builder.Property(x=>x.ImageUrl).IsRequired(true).HasMaxLength(100);    

        }
    }
}
