using DomainLayer.Entity.Order;

namespace EcommerceAPIS.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public string Status { get; set; } 
        public string ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }                                    
        public ICollection<OrderItemDto> Items { get; set; }

        public decimal SubTotal { get; set; }
      
       
        public string paymentIntentId { get; set; } 
        public decimal Total { get; set; }
    }
}
