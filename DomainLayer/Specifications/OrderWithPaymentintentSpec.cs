using DomainLayer.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specifications
{
    public class OrderWithPaymentintentSpec:BaseSpec<Order>
    {
        public OrderWithPaymentintentSpec(string paymentIntent)
            :base(o=>o.paymentIntentId== paymentIntent)
        {
           
        }
    }
}
