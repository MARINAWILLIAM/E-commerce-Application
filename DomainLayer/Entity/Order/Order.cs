using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity.Order
{
    public class Order:BaseEntity
    {
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            //intialize
            BuyerEmail = buyerEmail;
           
           
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            this.paymentIntentId = paymentIntentId;
        }

        public Order()
        {
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set;}=DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.pending;
        public Address ShippingAddress { get; set; } 
       // public int DeliveryMethodId { get; set; }//ForignKey
        //any relation in sql one many  all kda 
        //unique constrain
       // in here one to one

        public DeliveryMethod DeliveryMethod { get; set; }//navigation prop refer to table
         //ICollection<Order>
        //aalshan deh will make
        public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();
        //unique items items order unique hashset#
       public decimal SubTotal { get; set; }
        //[NotMapped]
        //public decimal Total  => SubTotal + DeliveryMethod.Cost; 
        public decimal GetTotal() 
            => SubTotal + DeliveryMethod.Cost;
        public string paymentIntentId { get; set; }
            //=string.Empty;
                                            



    }
}
