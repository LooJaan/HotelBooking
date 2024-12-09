using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData;

namespace AppView.Controllers
{
    public class TinTucsController : Controller
    {
        private readonly HotelDbContext _context;

        public TinTucsController(HotelDbContext context)
        {
            _context = context;
        }

		public async Task<IActionResult> MyView()
		{
			return View(await _context.tinTucs.ToListAsync());

		}
		public async Task<IActionResult> XemThem(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var tinTuc = await _context.tinTucs
				.FirstOrDefaultAsync(m => m.ID == id);
			if (tinTuc == null)
			{
				return NotFound();
			}

			return View(tinTuc);
		}

		// GET: TinTucs
		public async Task<IActionResult> Index()
        {
            return PartialView(await _context.tinTucs.ToListAsync());
        }

        // GET: TinTucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.tinTucs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return PartialView(tinTuc);
        }

        // GET: TinTucs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: TinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenTinTucChinh,TenTinTucPhu,HinhAnh1,HinhAnh2,HinhAnh3,NoiDungNgan,NoiDungChiTiet1,NoiDungChiTiet2,NoiDungChiTiet3,NoiDungChiTiet4,TrangThai")] TinTuc tinTuc, IFormFile hinhanh1, IFormFile hinhanh2, IFormFile hinhanh3 )
        {
            if (ModelState.IsValid)
            {
				if (hinhanh1 != null && hinhanh1.Length > 0)
				{
					// Lưu file vào thư mục trên server
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/about", hinhanh1.FileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await hinhanh1.CopyToAsync(stream);
					}
					// Lưu đường dẫn file vào database
					tinTuc.HinhAnh1 = hinhanh1.FileName;
				}

				if (hinhanh2 != null && hinhanh2.Length > 0)
				{
					// Lưu file vào thư mục trên server
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/about", hinhanh2.FileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await hinhanh2.CopyToAsync(stream);
					}
					// Lưu đường dẫn file vào database
					tinTuc.HinhAnh2 = hinhanh2.FileName;
				}

				if (hinhanh3 != null && hinhanh3.Length > 0)
				{
					// Lưu file vào thư mục trên server
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/about", hinhanh3.FileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await hinhanh3.CopyToAsync(stream);
					}
					// Lưu đường dẫn file vào database
					tinTuc.HinhAnh3 = hinhanh3.FileName;
				}

				_context.Add(tinTuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tinTuc);
        }

        // GET: TinTucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.tinTucs.FindAsync(id);
            if (tinTuc == null)
            {
                return NotFound();
            }
            return PartialView(tinTuc);
        }

        // POST: TinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenTinTucChinh,TenTinTucPhu,HinhAnh1,HinhAnh2,HinhAnh3,NoiDungNgan,NoiDungChiTiet1,NoiDungChiTiet2,NoiDungChiTiet3,NoiDungChiTiet4,TrangThai")] TinTuc tinTuc)
        {
            if (id != tinTuc.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tinTuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinTucExists(tinTuc.ID))
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
            return View(tinTuc);
        }

        // GET: TinTucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.tinTucs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // POST: TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinTuc = await _context.tinTucs.FindAsync(id);
            if (tinTuc != null)
            {
                _context.tinTucs.Remove(tinTuc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinTucExists(int id)
        {
            return _context.tinTucs.Any(e => e.ID == id);
        }
    }
}
