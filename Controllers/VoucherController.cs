using Microsoft.AspNetCore.Mvc;
using MiniAccountSystem.Models;
using MiniAccountSystem.Services;
using MiniAccountSystem.Services.Vouchers;

namespace MiniAccountSystem.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Accounts = await _voucherService.GetAccountsAsync();
            return View(new Voucher { VoucherDate = DateTime.Today });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                await _voucherService.SaveVoucherAsync(voucher);
                return RedirectToAction("Index");
            }

            ViewBag.Accounts = await _voucherService.GetAccountsAsync();
            return View(voucher);
        }
    }
}
