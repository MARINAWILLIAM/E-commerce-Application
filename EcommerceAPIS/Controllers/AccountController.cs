using AutoMapper;
using DomainLayer.Identity;
using DomainLayer.Services;
using EcommerceAPIS.Dtos;
using EcommerceAPIS.Errors;
using EcommerceAPIS.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceAPIS.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly SignInManager<Appuser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        //allow depande security services 3an tare2 AddAuthentication
        public AccountController(UserManager<Appuser> userManager
            ,SignInManager<Appuser> signInManager,
            ITokenServices tokenServices,
            IMapper mapper)
              
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(LoginDto loginDto)
        {
            var user=await _userManager.FindByEmailAsync(loginDto.Email);
            //tell usermanger to find by email email eli dakhal
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }
            return Ok(new AppUserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                token =await  _tokenServices.CreateToken(user,_userManager)

            });
            
            
        }
        [HttpPost("register")]
        public async Task<ActionResult<AppUserDto>> register(registerDto register)
        {
            if (CheckEmailExists(register.email).Result.Value)
            {
                return BadRequest(new ApiValidationErrorResponse() { Errors=new string[] {"This Email Id Already in use!!"} });
            }
            //create user in data base man no3 appuser
            var user = new Appuser()
            {
                DisplayName = register.DisplayName,
                Email = register.email,
                UserName = register.email.Split('@')[0],
                PhoneNumber = register.phoneNumber

          };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(new AppUserDto()
            {
                DisplayName= user.DisplayName,
                Email = user.Email,
                token = await _tokenServices.CreateToken(user, _userManager)
            });
        }
      [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]

        [HttpGet]
        public async Task<ActionResult<AppUserDto>> GetCurrentUser()
        {
         var email=User.FindFirstValue(ClaimTypes.Email);
            var  user= await _userManager.FindByEmailAsync(email);
            //prop shyla user amal login 
            return Ok(new AppUserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                token = await _tokenServices.CreateToken(user, _userManager)
            });

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpGet("address")]
        public async Task<ActionResult<AddressDtos>> GetUserAddress()
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            //var user = await _userManager.FindByEmailAsync(email); 
            var user = await _userManager.FindUsersWithAddress(User);
            var address= _mapper.Map<Address,AddressDtos>(user.Address);
            return Ok(address);
      

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpPut("address")]
        public async Task<ActionResult<AddressDtos>> UpdateUserAddress(AddressDtos updateaddress)
        {
            var address = _mapper.Map<AddressDtos, Address>(updateaddress);
            var user = await _userManager.FindUsersWithAddress(User);
            if (user.Address != null)
            {
                address.Id = user.Address.Id;
                //address.Id= user.Address.Id ;
            }
            
            user.Address = address;
            var result=await _userManager.UpdateAsync(user);
            //null           set addres new feh 
            //added law ala way adema
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }
        
            return Ok(updateaddress);


        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string Email)
        {
      
           return await _userManager.FindByEmailAsync(Email) is not null;

        }
    }
}
