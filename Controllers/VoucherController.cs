using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniAccountSystem.Models;
using MiniAccountSystem.Services.ModuleAccess;
using MiniAccountSystem.Services.Vouchers;

namespace MiniAccountSystem.Controllers
{
    [Authorize(Roles = "Admin,Accountant,Viewer")]
    public class VoucherController : Controller
    {
        private readonly IVoucherService _voucherService;
        private readonly IModuleAccessService _accessService;
        private readonly UserManager<IdentityUser> _userManager;

        public VoucherController(
            IVoucherService voucherService,
            IModuleAccessService accessService,
            UserManager<IdentityUser> userManager)
        {
            _voucherService = voucherService;
            _accessService = accessService;
            _userManager = userManager;
        }

        private async Task<bool> HasModuleAccess(string moduleName)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            return await _accessService.HasAccessAsync(role, moduleName);
        }

        public async Task<IActionResult> Index()
        {
            if (!await HasModuleAccess("VoucherEntry"))
                return RedirectToAction("AccessDenied", "Account");

            var vouchers = await _voucherService.GetAllVouchersAsync();
            return View(vouchers);
        }

        public async Task<IActionResult> Create()
        {
            if (!await HasModuleAccess("VoucherEntry"))
                return RedirectToAction("AccessDenied", "Account");

            ViewBag.Accounts = await _voucherService.GetAccountsAsync();
            return View(new Voucher { VoucherDate = DateTime.Today });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Voucher voucher)
        {
            if (!await HasModuleAccess("VoucherEntry"))
                return RedirectToAction("AccessDenied", "Account");

            if (ModelState.IsValid)
            {
                await _voucherService.SaveVoucherAsync(voucher);
                return RedirectToAction("Index");
            }

            ViewBag.Accounts = await _voucherService.GetAccountsAsync();
            return View(voucher);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await HasModuleAccess("VoucherEntry"))
                return RedirectToAction("AccessDenied", "Account");

            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if (voucher == null) return NotFound();
            return View(voucher);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!await HasModuleAccess("VoucherEntry"))
                return RedirectToAction("AccessDenied", "Account");

            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if (voucher == null) return NotFound();
            return View(voucher);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Voucher voucher)
        {
            if (!await HasModuleAccess("VoucherEntry"))
                return RedirectToAction("AccessDenied", "Account");

            if (!ModelState.IsValid) return View(voucher);
            await _voucherService.UpdateVoucherAsync(voucher);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await HasModuleAccess("VoucherEntry"))
                return RedirectToAction("AccessDenied", "Account");

            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if (voucher == null) return NotFound();
            await _voucherService.DeleteVoucherAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
