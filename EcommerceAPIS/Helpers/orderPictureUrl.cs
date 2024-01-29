using AutoMapper;
using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using EcommerceAPIS.Dtos;

namespace EcommerceAPIS.Helpers
{
    public class orderPictureUrl : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public orderPictureUrl(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.product.PictureUrl))
            {
                return $"{_configuration["baseurl"]}{source.product.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
