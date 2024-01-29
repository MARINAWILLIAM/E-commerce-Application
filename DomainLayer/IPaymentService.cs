using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId);
        Task <Order> UpdatePaymentIntentToSucceededOrFailed(string PaymentIntentId, bool isSucceeded);
        //alshan nmsak elbasket 


    }
}
