using DomainLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specifications
{
    public class ProductSpecIncludes:BaseSpec<ProductTable>
    {
        //warsat kolo

        public ProductSpecIncludes(productwithspecparams specparams)
       : base(p =>
        (string.IsNullOrEmpty(specparams.Search) || p.Name.ToLower().Contains(specparams.Search)) &&
             (!specparams.brandId.HasValue || p.productBrandId == specparams.brandId.Value) &&
              (!specparams.typeId.HasValue || p.ProductTypeId == specparams.typeId.Value))
       
       
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.producttype);
            AddOrderBy(p => p.Name);//defaaultsort
            if (!string.IsNullOrEmpty(specparams.sort))
            {
                switch(specparams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                       // OrderBy=p=>p.Price;
                        break;
                    case "priceDesc":
                        AddOrderByDESC(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            //total product=100 pagesize=10 pageindex=3
            ApplyPagnation(specparams.PageSize*(specparams.pageIndex-1),specparams.PageSize);

        }
        public ProductSpecIncludes(int id):base(p=>p.Id==id) 
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.producttype);
            
        }

    }
}
