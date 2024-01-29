using DomainLayer.Entity;
using DomainLayer.Entity.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryLayer.DataBaseHandler
{
    public static class StoreContextSeeding
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {
           if(!dbContext.Brands.Any())
                //law dbset fadya htrga3 false 
                //law feha haga wahada htrga3 true
                //law fadya a3mal seeding law la khlas 
            {
                var brandsData = File.ReadAllText("../RepositoryLayer/DataBaseHandler/DataSeeding/brands.json");
                //json file to string  bt2rah kda
                //mahtgen nhwlha laslha eli eh 
                var brands = JsonSerializer.Deserialize<List<ProductBrandTable>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await dbContext.Set<ProductBrandTable>().AddAsync(brand);

                        //seed for brand
                        //h3di ala kol brand adefo fe database
                    }
                    await dbContext.SaveChangesAsync();
                }
                //mara wahada y8uar kol status
            }
            if (!dbContext.Types.Any())
            //law dbset fadya htrga3 false 
            //law feha haga wahada htrga3 true
            //law fadya a3mal seeding law la khlas 
            {
                var TypesData = File.ReadAllText("../RepositoryLayer/DataBaseHandler/DataSeeding/types.json");
                //json file to string  bt2rah kda
                //mahtgen nhwlha laslha eli eh 
                var types = JsonSerializer.Deserialize<List<ProductTypeTable>>(TypesData);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await dbContext.Set<ProductTypeTable>().AddAsync(type);

                        //seed for brand
                        //h3di ala kol brand adefo fe database
                    }
                    await dbContext.SaveChangesAsync();
                }
                //mara wahada y8uar kol status

            }
            if (!dbContext.Products.Any())
            //law dbset fadya htrga3 false 
            //law feha haga wahada htrga3 true
            //law fadya a3mal seeding law la khlas 
            {
                var productsData = File.ReadAllText("../RepositoryLayer/DataBaseHandler/DataSeeding/products.json");
                //json file to string  bt2rah kda
                //mahtgen nhwlha laslha eli eh 
                var Products = JsonSerializer.Deserialize<List<ProductTable>>(productsData);
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        await dbContext.Set<ProductTable>().AddAsync(product);

                        //seed for brand
                        //h3di ala kol brand adefo fe database
                    }
                    await dbContext.SaveChangesAsync();
                }
                //mara wahada y8uar kol status
            }
            if (!dbContext.DeliveryMethod.Any())
            {
                var deliverymethodData = File.ReadAllText("../RepositoryLayer/DataBaseHandler/DataSeeding/delivery.json");

                var deliverymethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverymethodData);
                if (deliverymethod?.Count > 0)
                {
                    foreach (var dmethod in deliverymethod)
                    {
                        await dbContext.Set<DeliveryMethod>().AddAsync(dmethod);


                    }
                    await dbContext.SaveChangesAsync();
                }

            }

        }
    }
}
