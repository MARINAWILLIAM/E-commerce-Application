using AutoMapper;
using DomainLayer.Entity;
using EcommerceAPIS.Dtos;

namespace EcommerceAPIS.Helpers
{
    //mahdash byklamo 
    public class Productpictureurlresolve : IValueResolver<ProductTable, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public Productpictureurlresolve(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //resolve picture url bta3 project
        public string Resolve(ProductTable source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)) {
                return $"{_configuration["baseurl"]}{source.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
