using DomainLayer.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services
{
    public interface ITokenServices
    {
        //take user generate leh token dah
        Task<string> CreateToken(Appuser user,UserManager<Appuser> userManager);
    }
}
