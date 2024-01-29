using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using DomainLayer.Repo;
using DomainLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entity.Order;
using DomainLayer;
using DomainLayer.Specifications;

namespace ServicesLayer.Order
{
    public class OrderService : IOrderServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitofwork _unitofwork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository
            ,IUnitofwork unitofwork,
            IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
           _unitofwork = unitofwork;
           _paymentService = paymentService;
        }
        public async Task<DomainLayer.Entity.Order.Order?> CreateOrderAsync(string buyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            //1-GET BASKET FROM BASKET REPO
            var basket = await _basketRepository.GetBasketById(BasketId);

            //ana get basket kda
            //item in basket w amlo orderitem 
            //2- GET SELECTED ITEMS AT BASKET FROM PRODUCTS REPO
            var orderItems =new List<OrderItem>();
            if(basket?.Items?.Count>0)
            {
                foreach(var item in basket.Items)
                {
                    var product = await _unitofwork.Repository<ProductTable>().GetByIdAsync(item.Id);
                    var productitemorder= new ProductItem(product.Id, product.Name,product.PictureUrl);

                   var orderItem= new OrderItem(productitemorder,product.Price,item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
           //3- CALCULATE SUBTOTAL
           var subtotal= orderItems.Sum(item=>item.Price*item.Quantity);
            //4- GET DELIVERY METHOD FROM DELIVERYMETHODS REPO
            var deliverymethod = await _unitofwork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
            //5- CREATE ORDER
            var spec = new OrderWithPaymentintentSpec(basket.paymentIntentId);
            var ExistingOrder = await _unitofwork.Repository<DomainLayer.Entity.Order.Order>().GetEntityWithSpecAsync(spec);
            if(ExistingOrder != null)
            {
                _unitofwork.Repository<DomainLayer.Entity.Order.Order>().Delete(ExistingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(BasketId);
            }
            var order = new DomainLayer.Entity.Order.Order(buyerEmail, ShippingAddress, deliverymethod, orderItems, subtotal,basket.paymentIntentId);
            await _unitofwork.Repository<DomainLayer.Entity.Order.Order>().Add(order);
            
            //6- SAVE TO DATABASE[TODO]

          var result=await  _unitofwork.Complete();
            if (result <= 0)  return null; 
            return order;













        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod()
        {
            var deliverymethods= await _unitofwork.Repository<DeliveryMethod>().GetAllAsync();
            return deliverymethods;
        }

        public async Task<DomainLayer.Entity.Order.Order> GetOrderByIdForSpecUser(int OrderId, string buyerEmail)
        {
            var spec = new OrderSpec(OrderId,buyerEmail);
            var order = await _unitofwork.Repository<DomainLayer.Entity.Order.Order>().GetEntityWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<DomainLayer.Entity.Order.Order>> GetOrderForUser(string buyerEmail)
        {
            var spec = new OrderSpec(buyerEmail);
           var order=await _unitofwork.Repository<DomainLayer.Entity.Order.Order>().GetAllWithSpecAsync(spec);
          
            return order;
        }
    }
}
