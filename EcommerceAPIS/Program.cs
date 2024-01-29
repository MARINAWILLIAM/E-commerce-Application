using DomainLayer.Entity;
using DomainLayer.Identity;
using DomainLayer.Repo;
using EcommerceAPIS.Errors;
using EcommerceAPIS.Extentions;
using EcommerceAPIS.Helpers;
using EcommerceAPIS.MiddleWares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer;
using RepositoryLayer.DataBaseHandler;
using RepositoryLayer.DataBaseHandler.Identity;
using RepositoryLayer.Identity;
using StackExchange.Redis;
using System.Text;

namespace EcommerceAPIS
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                         .AddJsonFile("appsettings.json")
                         .Build();
            //create builder app obj from web application kestral
            //eli hybani web application with preconfigure 
            var builder = WebApplication.CreateBuilder(args);
            //service badd gwaha eli gwa builder
            // Add services to the container.
            //before build

            #region confservices
            
            builder.Services.AddControllers();
         
            builder.Services.AddApplicationServices();
            builder.Services.AddDbContext<StoreContext>(options =>
            { 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<ApplicationIdentityDbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
           
                 options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        //parmetre validate beha eltoken bt3i
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidationIssure"],
                       
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidationAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"])),

                    };
                    

                });
            builder.Services.AddIdentity<Appuser, IdentityRole>(options =>
            {
                options.Password.RequireDigit= true;    
                options.Password.RequireUppercase= true;
                options.Password.RequireLowercase= true;
                options.Password.RequireNonAlphanumeric= true;
            })

                //configure identity system 
                .AddEntityFrameworkStores<ApplicationIdentityDbcontext>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            //imple bt3at store deh impEntityFramework llinterface dah iuserstore


            //allow Apis services
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            ///
            builder.Services.AddScoped(typeof(IBasketRepository),typeof(BasketRepo));

            builder.Services.SwaggerServices();
            builder.Services.AddSingleton<IConnectionMultiplexer>(
                options =>
                {
                    var connection = builder.Configuration.GetConnectionString("Redis");
                  return ConnectionMultiplexer.Connect(connection);

                });

            #endregion
            //before bulit


            var app = builder.Build();//tla3 kesteral gwaha kol services eli hadtha
            using var scope = app.Services.CreateScope(); //maskt services eli scoped 
            //gwaha services provider kol services eli sh8la scope
            //container llservices   
           var services=scope.ServiceProvider;//obj fe kol services
            var loggerfactory=services.GetRequiredService<ILoggerFactory>();
            //obj from loggerfactyory
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>();
                var IdentityDbcontext = services.GetRequiredService<ApplicationIdentityDbcontext>();
                //obj from dbcontext
                //StoreContext dbcontext = new StoreContext();
                //ayz options mash h3raf ab3tha htlob man clr y3mal obj store context 
                //htlob Explicitly man clr
                //h3mal eli by3malo clr 
                //lazm adelo options deh
                await dbContext.Database.MigrateAsync();//update Database
                var usermanager=services.GetRequiredService<UserManager<Appuser>>();
                await AppidentityDataSeeding.seedusersasync(usermanager);
                    await IdentityDbcontext.Database.MigrateAsync();
                await StoreContextSeeding.SeedAsync(dbContext);
                //momkan tb3tlha elobj eli amlto man dbase
                //mara wahda seeding 

            }
            catch (Exception ex) //ex ref byshwar alah
            {
                var logger=loggerfactory.CreateLogger<Program>();
                //h3mal logger ala mostwa el program
                logger.LogError(ex,"an error occured during apply the Migration");

            }
            //after
            //bytla3 app bta3i
            // Configure the HTTP request pipeline.
            #region confi
            app.UseMiddleware<ExceptionMiddleWare>();//awl wahada hy3di alha my request
            if (app.Environment.IsDevelopment()) 
            {
                //custom middleware 
                //use service in swagger mahtag to sh8len depdany injection
                app.SwaggerMiddleware();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
           
            app.UseHttpsRedirection();//law ay request http thwalo https

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("MyPolicy");
            app.MapControllers();//routing tnfez endpoint 
            //bt3tmad ala route to get the request to the right place
            //a5tsar lldol
            // like use routing match request and use Endpoints  execute match dah
            //to tell clr route fe kol endpoints  use deh 
            //btrouh tnfaz roue eli mawgoda 3amd controller

            #endregion
            app.Run();
        }
    }
}