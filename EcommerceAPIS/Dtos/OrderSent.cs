using DomainLayer.Entity.Order;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPIS.Dtos
{
    public class OrderSent
    {
        [Required] 
        public string BasketId { get; set; }
       
        public int DeliveryMethodId { get; set; }
      
        public AddressDtos shipToAddress { get; set;}
    }
    }

