using DomainLayer.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DataBaseHandler.Confi
{
    public class Typeconfi: IEntityTypeConfiguration<ProductTypeTable>
    {
        public void Configure(EntityTypeBuilder<ProductTypeTable> builder)
        {
            builder.Property(B => B.Name).IsRequired();
            builder.HasIndex(b => b.Name).IsUnique();
        }
    }
    
}
