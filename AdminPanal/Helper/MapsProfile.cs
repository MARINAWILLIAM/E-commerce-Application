using AdminPanal.Models;
using AutoMapper;
using DomainLayer.Entity;
using Stripe;

namespace AdminPanal.Helper
{
    public class MapsProfile:Profile
    {
        public MapsProfile()
        {
            CreateMap<ProductTable,ProductViewModel>().ReverseMap();

		}
    }
}
