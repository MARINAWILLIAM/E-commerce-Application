using DomainLayer;
using DomainLayer.Repo;
using DomainLayer.Services;
using EcommerceAPIS.Errors;
using EcommerceAPIS.Helpers;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using ServicesLayer;
using ServicesLayer.Caching;
using ServicesLayer.Order;
using ServicesLayer.Payment;

namespace EcommerceAPIS.Extentions
{
    public  static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // builder.Services.AddAutoMapper (m=>m.AddProfile(new MappingProfile()));
            services.AddAutoMapper(typeof(MappingProfile));

            //obj man class bynfaz imapper  yhoto gwa mapp

            //builder.Services.AddScoped<IGenericRepository<ProductTable>,GenericRepository<ProductTable>>();
            //builder.Services.AddScoped<IGenericRepository<ProductTypeTable>, GenericRepository<ProductTypeTable>>();
            //builder.Services.AddScoped<IGenericRepository<ProductBrandTable>, GenericRepository<ProductBrandTable>>();
           // services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(ITokenServices), typeof(TokenServices));
            services.AddScoped(typeof(IPaymentService), typeof(PaymentServices));
            services.AddSingleton(typeof(IResponseCacheServices), typeof(ResponseCacheService));
            services.AddScoped(typeof(IUnitofwork), typeof(UnitOfWork));
            services.AddScoped(typeof(IOrderServices), typeof(OrderService));
            /// kestreal web application
            //default configuration bt3mal obj dah
            //options Behavior llApi bta3na
            services.Configure<ApiBehaviorOptions>(
                //take one par
                //in options invalid model state response factory
                //factory by3mal response  validation error change Behavior 
                options =>
                // options deh 3andha eh elfactory elms2al ala response bta3 endpoint eli 3andha invalid model state
                {
                    options.InvalidModelStateResponseFactory = (ActionContext) =>
                    //ma3ya contect feh kol hag fe action hgeb mano error
                    {
                        //                            dic  ayzkol pa la 
                        var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)//key value pair ll 3andha mashkal only
                                      .SelectMany(E => E.Value.Errors)
                                      .Select(e => e.ErrorMessage).ToArray();

                        //par state bt3thom not valid n3adi ala kol par w na5od error
                        //gwa kol haga t5os action
                        var ValidationErrorResponse = new ApiValidationErrorResponse()
                        {
                            Errors = errors
                        };
                        return new BadRequestObjectResult(ValidationErrorResponse);
                    };
                    //one par b3bar 3an context acti on eli execute now


                }
            );
            return services;
        }
    }
}
