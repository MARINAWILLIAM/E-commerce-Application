using DomainLayer.Entity;
using DomainLayer.Repo;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class BasketRepo : IBasketRepository
    {
        private readonly IDatabase _DataBase;

        public BasketRepo(IConnectionMultiplexer redisdb)
            //object 
        {
           _DataBase=redisdb.GetDatabase();
            //maskna database bta3t elredis
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
           return await _DataBase.KeyDeleteAsync(basketId);
            //delete from database by id
        }

        public async Task<CustomerBasket?> GetBasketById(string basketId)
        {
            var basket=await _DataBase.StringGetAsync(basketId);
            //json
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
     var createorupdate=       await _DataBase.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));
            if (!createorupdate) return null;
            return await GetBasketById(basket.Id);
                ///////////////////////////////////////ahwlha ljson
        }
    }
}
