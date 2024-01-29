using AutoMapper;
using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using DomainLayer.Identity;
using EcommerceAPIS.Dtos;
using System.Net.Sockets;

namespace EcommerceAPIS.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //kol mapp
            CreateMap<ProductTable, ProductDto>()
                .ForMember(d=>d.productBrand,o=>o.MapFrom(s=>s.productBrand.Name))
                .ForMember(d => d.productType, o => o.MapFrom(s => s.producttype.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom< Productpictureurlresolve>());

            CreateMap<DomainLayer.Identity.Address, AddressDtos>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto,Basketitem>();
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                 .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
                 ;
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.product.PictureUrl))
                 .ForMember(d => d.PictureUrl, o => o.MapFrom<orderPictureUrl>());
            ;
            CreateMap<AddressDtos, DomainLayer.Entity.Order.Address>();
        }
    }
}
