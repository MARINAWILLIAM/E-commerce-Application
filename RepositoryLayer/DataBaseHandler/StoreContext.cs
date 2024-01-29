using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataBaseHandler.Confi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DataBaseHandler
{
    public class StoreContext:DbContext
    {

        //mash 3ando tables security
        //identity dbcontext
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("");
        //}
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FluentApis
            //mash ahla haga
            //modelBuilder.ApplyConfiguration(new ProductConfi());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }                       //btgeb kol class fe project bynfaz interface   Apply kol confi classes                            //project eli bytnfaz dlwati
        public DbSet<ProductBrandTable> Brands { get; set; }
        public DbSet<ProductTypeTable> Types { get; set; }
        public DbSet<ProductTable> Products { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethod { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Order> Order { get; set; }

    }
}
