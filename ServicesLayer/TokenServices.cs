using DomainLayer.Identity;
using DomainLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateToken(Appuser user, UserManager<Appuser> userManager)
        {
            //start create token
            //private claims[userdefined]
            var authClaims=new List<Claim>() { 

                new Claim(ClaimTypes.GivenName,user.DisplayName),
                  new Claim(ClaimTypes.Email,user.Email)
                  //add two claims
            };
            var userroles = await userManager.GetRolesAsync(user);
            foreach (var role in userroles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            //make key 
            var authKey=new SymmetricSecurityKey(Encoding .UTF8.GetBytes(_configuration["JWT:key"]));
            //CREATE TOKEN
            var token = new JwtSecurityToken(
                //take somthings
                //make 3 register claims
                //go to appseting w hank hahtot register claims
                //need to header payload sig to make token
                //tell claims eli 3andna  payload
                issuer: _configuration["JWT:ValidationIssure"],
                audience: _configuration["JWT:ValidationAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                //private claism
                claims: authClaims,
                //sig=key
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                


                //object token

                ) ;
            //create token nafso
            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }
    }
}
