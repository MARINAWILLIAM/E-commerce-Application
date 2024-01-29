using AutoMapper;
using DomainLayer.Entity;
using DomainLayer.Repo;
using EcommerceAPIS.Dtos;
using EcommerceAPIS.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPIS.Controllers
{
    
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
           _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet/*("{id}")*/]//id=1 queryparam
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket=await _basketRepository.GetBasketById(id);
            return basket is null? new CustomerBasket(id): basket;
            //re create after expire make ctor take id and intialize it with id eli gay
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket([FromBody] CustomerBasketDto basket)
        {
            var mappedBasket=_mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var CreatedOrupdated = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (CreatedOrupdated is null) return BadRequest(new ApiResponse(400));
            return Ok(CreatedOrupdated);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string basketid)
        {
            return await _basketRepository.DeleteBasketAsync(basketid);
       
        }
    }
}
