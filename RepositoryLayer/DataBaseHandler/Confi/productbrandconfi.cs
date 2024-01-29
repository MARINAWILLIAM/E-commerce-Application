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
    public class productbrandconfi : IEntityTypeConfiguration<ProductBrandTable>
    {
       

        public void Configure(EntityTypeBuilder<ProductBrandTable> builder)
        {
            builder.Property(b => b.Name).IsRequired();
        }
    }
}
