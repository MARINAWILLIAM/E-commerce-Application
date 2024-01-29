using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataBaseHandler.Identity;
using RepositoryLayer.DataBaseHandler;
using DomainLayer.Identity;
using Microsoft.AspNetCore.Identity;
using DomainLayer;
using RepositoryLayer;
using AdminPanal.Helper;

namespace AdminPanal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped(typeof(IUnitofwork),typeof(UnitOfWork));
            builder.Services.AddAutoMapper(typeof(MapsProfile));
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            },ServiceLifetime.Transient);
            builder.Services.AddDbContext<ApplicationIdentityDbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            },ServiceLifetime.Transient);
            builder.Services.AddIdentity<Appuser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })

               //configure identity system 
               .AddEntityFrameworkStores<ApplicationIdentityDbcontext>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");

            app.Run();
        }
    }
}