using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shop.Api.DTOs.UsersDTOs;
using Shop.Api.Services;
using Shop.Core.Entities;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,JwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }
        //[HttpGet("roles")]
        //public async Task<IActionResult> CreateRole()
        //{
        //   await _roleManager.CreateAsync(new IdentityRole("Member"));
        //   await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    return Ok();
        //}
        //[HttpGet("admin")]
        //public async Task<IActionResult> CreateAdmin()
        //{
        //  var user = new AppUser
        //  {
        //      UserName = "Admin",
        //      FullName = "Super Admin",
        //      Email = "admin@mail.ru"

        //  };
        //    await _userManager.CreateAsync(user,"Admin123");
        //    await _userManager.AddToRoleAsync(user, "Admin");

        //    return Ok();

        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            AppUser user=await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return NotFound();
            }
            if(!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return BadRequest();

            }
            var roles = await _userManager.GetRolesAsync(user);
             
            return Ok(_jwtService.GenerateToken(user,roles));
        }

    }
}
