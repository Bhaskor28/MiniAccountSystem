using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniAccountSystem.Models;
using MiniAccountSystem.Services;
using MiniAccountSystem.Services.Vouchers;

namespace MiniAccountSystem.Controllers
{
    //[Authorize(Roles = "Admin,Accountant")]
    public class VoucherController : Controller
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        public async Task<IActionResult> Index()
        {
            var vouchers = await _voucherService.GetAllVouchersAsync(); // You’ll need to implement this method
            return View(vouchers);
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
        public async Task<IActionResult> Details(int id)
        {
            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if (voucher == null) return NotFound();
            return View(voucher);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if (voucher == null) return NotFound();
            return View(voucher);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Voucher voucher)
        {
            if (!ModelState.IsValid) return View(voucher);
            await _voucherService.UpdateVoucherAsync(voucher);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if (voucher == null) return NotFound();
            await _voucherService.DeleteVoucherAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
    }

}
