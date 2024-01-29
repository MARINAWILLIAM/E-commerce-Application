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
    internal class ProductConfi : IEntityTypeConfiguration<ProductTable>
    {
        //any class confi must implement interface IEntityConfiguration
        public void Configure(EntityTypeBuilder<ProductTable> builder)
        {
            //FluentApi of EF
            //hana gay product fa hbd2 bel product
            builder.HasOne(P => P.productBrand).WithMany()
                .HasForeignKey(P=>P.productBrandId)
              /*  OnDelete(deleteBehavior.setnull)*/;
            builder.HasOne(P => P.producttype).WithMany()
               .HasForeignKey(P => P.ProductTypeId);
            //any pro decimal mash by3raf yhdad no3ha ef fe sql 
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").HasMaxLength(100);
          
            builder.Property(p => p.PictureUrl).IsRequired(false).HasMaxLength(100);
        }
    }
}
