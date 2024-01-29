using DomainLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specifications
{
    public class productspecspecialforcount : BaseSpec<ProductTable>
    {
        public productspecspecialforcount(productwithspecparams specparams)
      : base(p =>
      //p=>p.name.tolower().contain("angular") htrouh criteria kda hyhotofe where

      (string.IsNullOrEmpty(specparams.Search) || p.Name.ToLower().Contains(specparams.Search)) &&
            (!specparams.brandId.HasValue || p.productBrandId == specparams.brandId.Value) &&
             (!specparams.typeId.HasValue || p.ProductTypeId == specparams.typeId.Value))
        {

        }
    }
}
        

