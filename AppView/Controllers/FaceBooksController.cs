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
    public class FaceBooksController : Controller
    {
        private readonly HotelDbContext _context;

        public FaceBooksController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: FaceBooks
        public async Task<IActionResult> Index()
        {
            return PartialView(await _context.faceBooks.ToListAsync());
        }

        // GET: FaceBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faceBook = await _context.faceBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faceBook == null)
            {
                return NotFound();
            }

            return PartialView(faceBook);
        }

        // GET: FaceBooks/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Link,Hinh,TrangThai")] FaceBook faceBook, IFormFile imageFile)
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
                    faceBook.Hinh = imageFile.FileName;
                }

                _context.Add(faceBook);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách
            }

            return View(faceBook);
        }

        // GET: FaceBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faceBook = await _context.faceBooks.FindAsync(id);
            if (faceBook == null)
            {
                return NotFound();
            }
            return PartialView(faceBook);
        }

        
        [HttpPost]
     
        public async Task<IActionResult> Edit(int id, [Bind("Id,Hinh,Link,TrangThai")] FaceBook faceBook , IFormFile imageFile)
        {
            if (id != faceBook.Id)
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
                    faceBook.Hinh = imageFile.FileName;
                }
                _context.Update(faceBook);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách
            }

            return View(faceBook);
        }

        // GET: FaceBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faceBook = await _context.faceBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faceBook == null)
            {
                return NotFound();
            }

            return View(faceBook);
        }

        // POST: FaceBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faceBook = await _context.faceBooks.FindAsync(id);
            if (faceBook != null)
            {
                _context.faceBooks.Remove(faceBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaceBookExists(int id)
        {
            return _context.faceBooks.Any(e => e.Id == id);
        }
    }
}
