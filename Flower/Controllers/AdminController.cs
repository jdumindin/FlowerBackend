using DataModel;
using Flower;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Flower.DTO;
using System.IdentityModel.Tokens.Jwt;
namespace SomethingIEnjoy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<AppUser> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInAsync(LogInRequest request)
        {
            AppUser? user = await userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return Unauthorized("Username Bad");
            }
            bool success = await userManager.CheckPasswordAsync(user, request.Password);
            if (!success)
            {
                return Unauthorized("Password Bad");
            }
            JwtSecurityToken token = await jwtHandler.GetSecurityTokenAsync(user);
            string jwtString = new JwtSecurityTokenHandler().WriteToken(token);
            LogInResponse response = new()
            {
                Success = true,
                Message = "Mom loves you <3",
                Token = jwtString
            };
            return Ok(response);
        }
    }
}