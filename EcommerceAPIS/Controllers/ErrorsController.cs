using EcommerceAPIS.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.DataBaseHandler;

namespace EcommerceAPIS.Controllers
{
  
    public class ErrorsController : BaseController
    {
        private readonly StoreContext _storeContext;

        public ErrorsController(StoreContext storeContext)
        {
            _storeContext = storeContext;
            //talk to database direct
        }
        [HttpGet("NotFound")]//get :api/Errors/NotFound
        public ActionResult GetNotFoundRequest()
        {
            var product = _storeContext.Products.Find(100);
            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }
        [HttpGet("ServerError")]//get :api/Errors/ServerError
        public ActionResult GetServerError()
        {
            var product = _storeContext.Products.Find(100);
            var producttoreturn = product.ToString();
            //exception
            return Ok(producttoreturn);
        }
        [HttpGet("BadRequest")]//get :api/Errors/BadRequest
        public ActionResult GetBadRequestRquest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("BadRequest/{id}")]//get :api/Errors/BadRequest/five
        //validtionerror from bad request
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }

    }
}
