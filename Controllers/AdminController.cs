using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniAccountSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AdminController(RoleManager<IdentityRole> roleManager,
                               UserManager<IdentityUser> userManager,
                               IConfiguration config)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _config = config;
        }

        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var model = new List<(string UserId, string Email, IList<string> Roles)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add((user.Id, user.Email, roles));
            }

            return View(model);
        }

        public async Task<IActionResult> AssignRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, role);

            // Optional: assign role-module access here via stored proc
            using SqlConnection con = new(_config.GetConnectionString("Connection"));
            using SqlCommand cmd = new("sp_AssignModuleToRole", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleName", role);
            cmd.Parameters.AddWithValue("@ModuleName", "Account"); // Example

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return RedirectToAction("Users");
        }
    }
}
