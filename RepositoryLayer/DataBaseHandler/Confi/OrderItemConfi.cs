using DomainLayer.Entity.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DataBaseHandler.Confi
{
    public class OrderItemConfi : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(oi => oi.product, product => product.WithOwner());
            builder.Property(oi=>oi.Price).HasColumnType("decimal(18,2)");
        }
    }
}
