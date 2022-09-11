using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<Admin> _userManager;
        public AdminController(UserManager<Admin> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(policy: "RequireAdminRole")]
        [HttpGet("users")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Include(r => r.Roles)
                .ThenInclude(r => r._Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    UserName = u.UserName,
                    Roles = u.Roles.Select(r => r._Role.Name).ToList()
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}
