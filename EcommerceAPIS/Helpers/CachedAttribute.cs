using DomainLayer;
using EcommerceAPIS.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace EcommerceAPIS.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter 
    {
        private readonly int _timetoliveinseconds;
       // private readonly IResponseCacheServices _responseCacheServices;

        public CachedAttribute(int timetoliveinseconds)
        {
            _timetoliveinseconds = timetoliveinseconds;
           // _responseCacheServices = responseCacheServices;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {                                       //lsa bytnfaz getproduct           //ref endpoint eli httfaz
            //abl ma y5od value y3mlaha binding gwa parameter
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheServices>();
            //make object from cashservice 
            //service 
            var CacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
             var CachedResponse = await cacheService.GetCashResponseAsync(CacheKey);
              if(!string.IsNullOrEmpty(CachedResponse))
            {
                //hrouh amsak endpoint  result bt3tha elgahaza 
                //object man class bynfaz interface
                //content result
                var contentResult = new ContentResult()
                {
                    Content = CachedResponse,
                    ContentType = "application/json",
                    StatusCode= 200
                };
                context.Result = contentResult;
                return;
                //please skip this endpoint and take it from content result
             }
            var ExecutedEndpointContext=   await next();//will execute endpoint
            if(ExecutedEndpointContext.Result is OkObjectResult okObjectResult) //var 

            {
                await cacheService.CashResponseAsync(CacheKey,okObjectResult.Value,TimeSpan.FromSeconds(_timetoliveinseconds));
            }










        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
           //meaning unique
           //{{url}}/api/Products?pageIndex=1&pageSize=5&sort=name 
           //1-urlpath          querystring
           var keyBuilder=new StringBuilder();
            //url path
            keyBuilder.Append(request.Path);//api/products
            foreach(var (key,value) in request.Query.OrderBy(x=>x.Key))
                //hykon matrtab
            {
                //pairs
                //api/products|pageIndex-1|pageSize-5|sort-name
                //api/products

                keyBuilder.Append($"{key}-{value}");
            }
            return keyBuilder.ToString();

        }
    }
}
