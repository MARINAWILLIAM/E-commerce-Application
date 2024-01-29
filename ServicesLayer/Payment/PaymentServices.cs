using DomainLayer;
using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using DomainLayer.Repo;
using DomainLayer.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Payment
{
    public class PaymentServices : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitofwork _unitofwork;
        private readonly IBasketRepository _basket;

        public PaymentServices(IConfiguration configuration,IUnitofwork unitofwork,IBasketRepository basket)
        {
            _configuration = configuration;
            _unitofwork = unitofwork;
            _basket = basket;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {

            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            //class stripe
            //take this function endpoint htnfaz function alshan nklam stripe
            var basket = await _basket.GetBasketById(BasketId);
            if (basket == null)  return null;
            var ShippingPrice = 0m;
            if(basket.DeliveryMethodId.HasValue)
            {
               var DeliveryMethod = await _unitofwork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                basket.ShippingCost = DeliveryMethod.Cost;
                ShippingPrice = DeliveryMethod.Cost;
            }
            if(basket?.Items.Count > 0)
            {
                foreach ( var item in basket.Items)
                {
                    var product = await _unitofwork.Repository<ProductTable>().GetByIdAsync(item.Id);
                    if(item.Price!=product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }
            PaymentIntent paymentIntent;
            var services = new  PaymentIntentService();
            if (string.IsNullOrEmpty(basket.paymentIntentId))//create lsa
            {
                //options
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)ShippingPrice * 100,
                    Currency="usd",
                    PaymentMethodTypes=new List<string>() { "card"}
                    
                };
                paymentIntent = await services.CreateAsync(options);
                basket.paymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
               

            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)ShippingPrice * 100,
                 

                };
                await services.UpdateAsync(basket.paymentIntentId,options);
             
            }
            await _basket.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<DomainLayer.Entity.Order.Order> UpdatePaymentIntentToSucceededOrFailed(string PaymentIntentId, bool isSucceeded)
        {
            var spec = new OrderWithPaymentintentSpec(PaymentIntentId);
           var order=await _unitofwork.Repository<DomainLayer.Entity.Order.Order>().GetEntityWithSpecAsync(spec);
            if(isSucceeded)
            {
                order.Status = OrderStatus.paymentReceived;
            }
            else { order.Status = OrderStatus.paymentFailed; }
             _unitofwork.Repository<DomainLayer.Entity.Order.Order>().Update(order);
           await  _unitofwork.Complete();
            return order;

        }

    }
}
