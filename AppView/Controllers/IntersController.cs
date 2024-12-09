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
    public class IntersController : Controller
    {
        private readonly HotelDbContext _context;

        public IntersController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: Inters
        public async Task<IActionResult> Index()
        {
            return PartialView(await _context.inters.ToListAsync());
        }

        // GET: Inters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inter = await _context.inters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inter == null)
            {
                return NotFound();
            }

            return PartialView(inter);
        }

        // GET: Inters/Create
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]

        public async Task<IActionResult> Create([Bind("Id,Link,Hinh,TrangThai")] Inter gG, IFormFile imageFile)
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

            var gg = await _context.inters.FindAsync(id);
            if (gg == null)
            {
                return NotFound();
            }
            return PartialView(gg);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Hinh,Link,TrangThai")] Inter gG, IFormFile imageFile)
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

        // GET: Inters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inter = await _context.inters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inter == null)
            {
                return NotFound();
            }

            return View(inter);
        }

        // POST: Inters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inter = await _context.inters.FindAsync(id);
            if (inter != null)
            {
                _context.inters.Remove(inter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterExists(int id)
        {
            return _context.inters.Any(e => e.Id == id);
        }
    }
}
