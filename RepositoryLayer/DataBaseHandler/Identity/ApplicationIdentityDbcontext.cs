using DomainLayer.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DataBaseHandler.Identity
{
    public class ApplicationIdentityDbcontext:IdentityDbContext<Appuser>
    {                                           //7 dbsets  user role inherit
      

        public ApplicationIdentityDbcontext(DbContextOptions<ApplicationIdentityDbcontext> options) : base(options)
        {
        }
    }
}
