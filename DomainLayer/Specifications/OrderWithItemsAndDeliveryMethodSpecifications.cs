using DomainLayer.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specifications
{
    public class OrderWithItemsAndDeliveryMethodSpecifications:BaseSpec<Order>
    {
        public OrderWithItemsAndDeliveryMethodSpecifications()
        {
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.ShippingAddress);
            AddOrderByDESC(O => O.OrderDate);
        }
        public OrderWithItemsAndDeliveryMethodSpecifications(string buyerEmail)
            : base(O => O.BuyerEmail == buyerEmail)
        {
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);

            AddOrderByDESC(O => O.OrderDate);
        }
        public OrderWithItemsAndDeliveryMethodSpecifications(int orderId, string buyerEmail)
            : base(O => (O.BuyerEmail == buyerEmail) && (O.Id == orderId))
        {
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);
        }

    }
}
