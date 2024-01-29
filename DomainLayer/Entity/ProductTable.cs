using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    public class ProductTable:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
         public int productBrandId { get; set; }//foreign Key not allow null
        public int ProductTypeId { get; set; }//foreign Key not allow null
        public ProductBrandTable productBrand  { get; set; }//navigational property
        public ProductTypeTable producttype { get; set; }//navigational property
    }
}
