using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Identity
{
    public class Appuser : IdentityUser
    {
        
        public string DisplayName { get; set; }
        public Address Address { get; set; }//one
        
        }

}
