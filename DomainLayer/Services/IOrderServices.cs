using DomainLayer.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services
{
    public interface IOrderServices
    {
        Task<Order?> CreateOrderAsync(string buyerEmail,string BasketId,int DeliveryMethodId,Address ShippingAddress);
        Task<IReadOnlyList<Order>> GetOrderForUser(string buyerEmail);
        Task<Order> GetOrderByIdForSpecUser(int OrderId,string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod();






    }
}
