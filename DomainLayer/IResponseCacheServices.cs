using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public interface IResponseCacheServices
    {
        //cashing ** get responsecashing
        Task CashResponseAsync(string CacheKey,object response,TimeSpan TimeToLive );//take 3 information 
        Task<string?> GetCashResponseAsync(string CacheKey);
        //take key return value
    }
}
