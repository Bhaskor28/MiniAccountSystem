using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniAccountSystem.Models;
using MiniAccountSystem.Services.AccountChartList;

namespace MiniAccountSystem.Controllers
{
    //[Authorize(Roles = "Admin,Accountant")]
    public class ChartOfAccountController : Controller
    {
        private readonly IAccountChartService _service;

        public ChartOfAccountController(IAccountChartService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountChart model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountChart model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
