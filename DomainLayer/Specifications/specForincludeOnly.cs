using DomainLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specifications
{
	public class specForincludeOnly: BaseSpec<ProductTable>
	{
        public specForincludeOnly()
        {
            Includes.Add(P => P.productBrand);
			Includes.Add(P => P.producttype);
		}
    }
}
