using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<Basketitem> Items { get; set; }
        public string paymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingCost { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
