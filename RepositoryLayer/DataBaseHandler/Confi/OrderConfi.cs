using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DataBaseHandler.Confi
{
    public class OrderConfi : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.OwnsOne(o => o.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());
            //one to one total both
            builder.Property(o => o.Status).HasConversion(
                //awl par status htt5zan shkal w trga3 shkal tani
                Ostatus => Ostatus.ToString(), //string hy5od values eli hana
                //tani par
                  Ostatus =>(OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus)
                  //return from database azay
        //eli yrga3 l orderstatus      btrga3 object                                    //parse string 

                );
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasMany(o => o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);


        }
    }
}
