using DomainLayer.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specifications
{
    public class OrderSpec:BaseSpec<Order>
    {
        public OrderSpec(string buyerEmail)
            : base(o=>o.BuyerEmail==buyerEmail)     
        {
           Includes.Add(o=>o.DeliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderByDESC(o => o.OrderDate);

        }
        public OrderSpec(int orderid,string buyerEmail)
           : base(o => o.BuyerEmail == buyerEmail&&o.Id==orderid)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
          

        }
    }
}
