using CsvHelper.Configuration;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flower.Data;

namespace Flower.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(FlowerDbContext db, UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpPost("Users")]
        public async Task<IActionResult> ImportUsersAsync()
        {
            (string name, string email) = ("Human", "realhuman@email.com");
            AppUser user = new AppUser()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (await userManager.FindByEmailAsync(email) is not null) return Ok(user);

            IdentityResult result = await userManager.CreateAsync(user, "P4$$w0rd");
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
