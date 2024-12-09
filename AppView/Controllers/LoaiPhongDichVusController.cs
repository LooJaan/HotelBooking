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
    public class LoaiPhongDichVusController : Controller
    {
        private readonly HotelDbContext _context;

        public LoaiPhongDichVusController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: LoaiPhongDichVus
        public async Task<IActionResult> Index()
        {
            var hotelDbContext = _context.LoaiPhongDichVu.Include(l => l.DichVu).Include(l => l.LoaiPhong);
            return PartialView(await hotelDbContext.ToListAsync());
        }

        // GET: LoaiPhongDichVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhongDichVu = await _context.LoaiPhongDichVu
                .Include(l => l.DichVu)
                .Include(l => l.LoaiPhong)
                .FirstOrDefaultAsync(m => m.DichVusID == id);
            if (loaiPhongDichVu == null)
            {
                return NotFound();
            }

            return PartialView(loaiPhongDichVu);
        }

        // GET: LoaiPhongDichVus/Create
        public IActionResult Create()
        {
            ViewData["DichVusID"] = new SelectList(_context.DichVus, "ID", "Hinh");
            ViewData["LoaiPhongsId"] = new SelectList(_context.LoaiPhongs, "MaLoaiPhong", "TenLoaiPhong");
            return PartialView();
        }

        // POST: LoaiPhongDichVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DichVusID,LoaiPhongsId")] LoaiPhongDichVu loaiPhongDichVu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiPhongDichVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DichVusID"] = new SelectList(_context.DichVus, "ID", "Hinh", loaiPhongDichVu.DichVusID);
            ViewData["LoaiPhongsId"] = new SelectList(_context.LoaiPhongs, "MaLoaiPhong", "TenLoaiPhong", loaiPhongDichVu.LoaiPhongsId);
            return View(loaiPhongDichVu);
        }

        // GET: LoaiPhongDichVus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhongDichVu = await _context.LoaiPhongDichVu.FindAsync(id);
            if (loaiPhongDichVu == null)
            {
                return NotFound();
            }
            ViewData["DichVusID"] = new SelectList(_context.DichVus, "ID", "Hinh", loaiPhongDichVu.DichVusID);
            ViewData["LoaiPhongsId"] = new SelectList(_context.LoaiPhongs, "MaLoaiPhong", "TenLoaiPhong", loaiPhongDichVu.LoaiPhongsId);
            return PartialView(loaiPhongDichVu);
        }

        // POST: LoaiPhongDichVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DichVusID,LoaiPhongsId")] LoaiPhongDichVu loaiPhongDichVu)
        {
            if (id != loaiPhongDichVu.DichVusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiPhongDichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiPhongDichVuExists(loaiPhongDichVu.DichVusID))
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
            ViewData["DichVusID"] = new SelectList(_context.DichVus, "ID", "Hinh", loaiPhongDichVu.DichVusID);
            ViewData["LoaiPhongsId"] = new SelectList(_context.LoaiPhongs, "MaLoaiPhong", "TenLoaiPhong", loaiPhongDichVu.LoaiPhongsId);
            return View(loaiPhongDichVu);
        }

        // GET: LoaiPhongDichVus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhongDichVu = await _context.LoaiPhongDichVu
                .Include(l => l.DichVu)
                .Include(l => l.LoaiPhong)
                .FirstOrDefaultAsync(m => m.DichVusID == id);
            if (loaiPhongDichVu == null)
            {
                return NotFound();
            }

            return View(loaiPhongDichVu);
        }

        // POST: LoaiPhongDichVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiPhongDichVu = await _context.LoaiPhongDichVu.FindAsync(id);
            if (loaiPhongDichVu != null)
            {
                _context.LoaiPhongDichVu.Remove(loaiPhongDichVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiPhongDichVuExists(int id)
        {
            return _context.LoaiPhongDichVu.Any(e => e.DichVusID == id);
        }
    }
}
