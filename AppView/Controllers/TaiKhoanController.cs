using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppView.ViewModels;

namespace AppView.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly HotelDbContext _context;

        public TaiKhoanController(HotelDbContext context)
        {
            _context = context;
        }

        public async  Task<IActionResult> DangNhap(string TaiKhoan, string MatKhau)
        {
			if (string.IsNullOrWhiteSpace(TaiKhoan) || string.IsNullOrWhiteSpace(MatKhau))
			{
				ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ tài khoản và mật khẩu.";
				return View();
			}

			var user = await _context.TaiKhoans.FirstOrDefaultAsync(t => t.TaiKhoan == TaiKhoan && t.MatKhau == MatKhau);

			if (user != null)
			{
				return RedirectToAction("TrangQuanLy", "TaiKhoan");
			}

			ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng.";
			return View();
		}
        public IActionResult TrangQuanLy()
        {
            return View();
        }
        // GET: TaiKhoan
        public async Task<IActionResult> Index()
        {
            var data = _context.TaiKhoans.ToList();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_IndexTaiKhoanPartial", data);
            }
            return View(data);

            //return PartialView(await _context.TaiKhoans.ToListAsync());
        }

        // GET: TaiKhoan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoann = await _context.TaiKhoans
                .FirstOrDefaultAsync(m => m.ID == id);
            if (taiKhoann == null)
            {
                return NotFound();
            }

            return PartialView(taiKhoann);
        }

        // GET: TaiKhoan/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: TaiKhoan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TaiKhoan,MatKhau")] TaiKhoann taiKhoann)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiKhoann);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taiKhoann);
        }

        // GET: TaiKhoan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoann = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoann == null)
            {
                return NotFound();
            }
            return PartialView(taiKhoann);
        }

        // POST: TaiKhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TaiKhoan,MatKhau")] TaiKhoann taiKhoann)
        {
            if (id != taiKhoann.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoann);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoannExists(taiKhoann.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taiKhoann);
        }

        // GET: TaiKhoan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoann = await _context.TaiKhoans
                .FirstOrDefaultAsync(m => m.ID == id);
            if (taiKhoann == null)
            {
                return NotFound();
            }

            return View(taiKhoann);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taiKhoann = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoann != null)
            {
                _context.TaiKhoans.Remove(taiKhoann);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoannExists(int id)
        {
            return _context.TaiKhoans.Any(e => e.ID == id);
        }
    }
}
