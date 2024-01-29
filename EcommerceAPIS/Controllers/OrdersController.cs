using AutoMapper;
using DomainLayer;
using DomainLayer.Entity.Order;
using DomainLayer.Services;
using EcommerceAPIS.Dtos;
using EcommerceAPIS.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace EcommerceAPIS.Controllers
{
 
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;
        private readonly IUnitofwork _unitofwork;

        public OrdersController(IOrderServices orderServices,IMapper mapper)
        {
            _orderServices = orderServices;
          _mapper = mapper;
        
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
          public async Task<ActionResult<OrderDto>> CreateOrder(OrderSent order)
        {
            var buyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var address=_mapper.Map<AddressDtos,Address>(order.shipToAddress);
            var Order=   await _orderServices.CreateOrderAsync(buyerEmail, order.BasketId, order.DeliveryMethodId, address);
            if(Order==null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(_mapper.Map<Order,OrderDto>(Order));
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrderForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            
            var Orders = await _orderServices.GetOrderForUser(buyerEmail);
            if (Orders == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(_mapper.Map <IReadOnlyList<Order>,IReadOnlyList<OrderDto>> (Orders));
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetSpecOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var Order = await _orderServices.GetOrderByIdForSpecUser(id,buyerEmail);
            if (Order == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Order, OrderDto>(Order));
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(DeliveryMethod), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {


            var DeliveryMethod = await _orderServices.GetDeliveryMethod();
            if (DeliveryMethod == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(DeliveryMethod);
        }
    }
}
