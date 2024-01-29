using DomainLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DataBaseHandler.Confi
{
    public class producttypeconfi : IEntityTypeConfiguration<ProductTypeTable>
    {
        public void Configure(EntityTypeBuilder<ProductTypeTable> builder)
        {
            builder.Property(t => t.Name).IsRequired();
        }
    }
}
