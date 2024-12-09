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
    public class LienHesController : Controller
    {
        private readonly HotelDbContext _context;

        public LienHesController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: LienHes
        public async Task<IActionResult> Index()
        {
            return PartialView(await _context.lienHes.ToListAsync());
        }

        // GET: LienHes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHe = await _context.lienHes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lienHe == null)
            {
                return NotFound();
            }

            return PartialView(lienHe);
        }

        // GET: LienHes/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: LienHes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SoDienThoai,LogoSDT,TrangThai")] LienHe lienHe , IFormFile imageFile)
        {
           /* if (!ModelState.IsValid)*/
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Lưu file vào thư mục trên server
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/icon", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn file vào database
                    lienHe.LogoSDT = imageFile.FileName;
                }
                _context.Add(lienHe);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách

            }
            return View(lienHe);
        }

        // GET: LienHes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHe = await _context.lienHes.FindAsync(id);
            if (lienHe == null)
            {
                return NotFound();
            }
            return PartialView(lienHe);
        }

        // POST: LienHes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SoDienThoai,LogoSDT,TrangThai")] LienHe lienHe , IFormFile imageFile)
        {
            if (id != lienHe.ID)
            {
                return NotFound();
            }

          /*  if (!ModelState.IsValid)*/
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Lưu file vào thư mục trên server
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/icon", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn file vào database
                    lienHe.LogoSDT = imageFile.FileName;
                }
                _context.Update(lienHe);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách

            }
            return View(lienHe);
           
        }

        // GET: LienHes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHe = await _context.lienHes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lienHe == null)
            {
                return NotFound();
            }

            return View(lienHe);
        }

        // POST: LienHes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lienHe = await _context.lienHes.FindAsync(id);
            if (lienHe != null)
            {
                _context.lienHes.Remove(lienHe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LienHeExists(int id)
        {
            return _context.lienHes.Any(e => e.ID == id);
        }
    }
}
