using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entity;

namespace RepositoryLayer.DataBaseHandler.Confi
{
	public class BrandConfi : IEntityTypeConfiguration<ProductBrandTable>
	{ 
		public void Configure(EntityTypeBuilder<ProductBrandTable> builder)
		{
			builder.Property(B => B.Name).IsRequired();
			builder.HasIndex(b => b.Name).IsUnique();
		}
	
	}
}
