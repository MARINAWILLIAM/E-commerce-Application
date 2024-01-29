using DomainLayer;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServicesLayer.Caching
{
    public class ResponseCacheService : IResponseCacheServices
    {
       private readonly IDatabase _database;

        public ResponseCacheService(IConnectionMultiplexer redis)
     
        {
            _database = redis.GetDatabase();
           
        }
        public async Task CashResponseAsync(string CacheKey, object response, TimeSpan TimeToLive)
        {
           if(response== null)return;
           var options=new JsonSerializerOptions() { PropertyNamingPolicy= JsonNamingPolicy.CamelCase };
            //to store in Redis in camelcase
            var SerializedResponse =JsonSerializer.Serialize(response,options);
            await _database.StringSetAsync(CacheKey, SerializedResponse, TimeToLive);
        }

        public async Task<string?> GetCashResponseAsync(string CacheKey)
        {
           var CashResponse=await _database.StringGetAsync(CacheKey);
            if (CashResponse.IsNullOrEmpty) return null;
            return CashResponse;//null Redis value struct not take null asln 
            //object is empty object struct empty

        }
    }
}
