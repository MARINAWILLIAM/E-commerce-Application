using DomainLayer;
using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using EcommerceAPIS.Dtos;
using EcommerceAPIS.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Climate;

namespace EcommerceAPIS.Controllers
{
 
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private const string _whSecret = "whsec_8a8a09dc2b198b07c612005da6cdf3f2491ef4514d8d13d3d81d6d747867dc56";

        public PaymentController(IPaymentService paymentService,ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
           _logger = logger;
        }

        [HttpPost]//?
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string id)
        {
            var basket=await _paymentService.CreateOrUpdatePaymentIntent(id);
            if(basket == null)
            {
                return BadRequest(new ApiResponse(400, "A Problem With Your Basket"));
            }
            return Ok(basket);
        }

        [HttpPost("webhook")]
        //stripe ynfzha
        public async Task<IActionResult> StripWebhook()
            //mash ayz ahdad na3 response
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _whSecret);
                var paymentintent=stripeEvent.Data.Object as PaymentIntent;
                DomainLayer.Entity.Order.Order order;
                // Handle the event
                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        order= await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentintent.Id, true);
                    _logger.LogInformation("payment is succeeded", paymentintent.Id);
                        break;
                    case Events.PaymentIntentPaymentFailed:
                        order= await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentintent.Id, false);
                    _logger.LogInformation("payment is failed", paymentintent.Id);



                        break;
                }

                return Ok();
            
           
        }
















    }

}
