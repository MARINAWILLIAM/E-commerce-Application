using EcommerceAPIS.Errors;
using System;
using System.Net;
using System.Text.Json;

namespace EcommerceAPIS.MiddleWares
{
    public class ExceptionMiddleWare
        {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _environment;

        //ctor lazm by5od 3 parameters
        //eli hyfaz deh clr eli request hymor beha
        public ExceptionMiddleWare(RequestDelegate next,ILogger<ExceptionMiddleWare> logger ,IHostEnvironment environment)
        {                          //next middleware ref to next middle ware                              //log expection in console           check ala env 
            _next = next;
          _logger = logger;
         _environment = environment;
        }
        //lazm ykon 3andha  func invoke async
        //lama y3adi ala middle ware hynfaz elfunc deh
        public async Task InvokeAsync(HttpContext context)
        {
            //law feh exception mash hy3raf yrouh law mash feh hyrouh
            try
            {
                await _next.Invoke(context);
                //delegate fire     context request 

            }  
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //log exception in database [production] 
                //wait response
                //catch context
                context.Response.ContentType = "application/json";//no3 response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //nazbt body obj mess ,stat class 
                var responsexception = _environment.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, 
                    ex.Message, ex.StackTrace.ToString()) :
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);
              var options= new JsonSerializerOptions() { PropertyNamingPolicy= 
                  JsonNamingPolicy.CamelCase };
               
                var json =JsonSerializer.Serialize(responsexception,options);
                await context.Response.WriteAsync(json);
                //only one

            };

        }
    }
}
