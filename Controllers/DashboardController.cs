using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MiniAccountSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            return role switch
            {
                "Admin" => View("AdminDashboard"),
                "Accountant" => View("AccountantDashboard"),
                "Viewer" => View("ViewerDashboard"),
                _ => RedirectToAction("AccessDenied", "Account")
            };
        }
    }
}
