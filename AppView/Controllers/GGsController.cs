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
    public class GGsController : Controller
    {
        private readonly HotelDbContext _context;

        public GGsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: GGs
        public async Task<IActionResult> Index()
        {
            return PartialView(await _context.gGs.ToListAsync());
        }

        // GET: GGs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gG = await _context.gGs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gG == null)
            {
                return NotFound();
            }

            return PartialView(gG);
        }

        // GET: GGs/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("Id,Link,Hinh,TrangThai")] GG gG, IFormFile imageFile)
        {

            if (!ModelState.IsValid)
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
                    gG.Hinh = imageFile.FileName;
                }

                _context.Add(gG);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách
            }

            return View(gG);
        }

        // GET: FaceBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gg = await _context.gGs.FindAsync(id);
            if (gg == null)
            {
                return NotFound();
            }
            return PartialView(gg);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Hinh,Link,TrangThai")] GG gG, IFormFile imageFile)
        {
            if (id != gG.Id)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Lưu file vào thư mục trên server
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn file vào database
                    gG.Hinh = imageFile.FileName;
                }
                _context.Update(gG);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách
            }

            return View(gG);
        }

        // GET: GGs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gG = await _context.gGs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gG == null)
            {
                return NotFound();
            }

            return View(gG);
        }

        // POST: GGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gG = await _context.gGs.FindAsync(id);
            if (gG != null)
            {
                _context.gGs.Remove(gG);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GGExists(int id)
        {
            return _context.gGs.Any(e => e.Id == id);
        }
    }
}
