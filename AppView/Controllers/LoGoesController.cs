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
    public class LoGoesController : Controller
    {
        private readonly HotelDbContext _context;

        public LoGoesController(HotelDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return PartialView(_context.loGos.ToList());
        }

        // GET: LoGo/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: LoGo/Create
        [HttpPost]
        
        public async Task<IActionResult> Create([Bind("ID,Logo,TrangThai")]  LoGo logo, IFormFile imageFile)
        {
            
            /*if (!ModelState.IsValid)*/
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
                    logo.Logo = imageFile.FileName;
                }

                _context.Add(logo);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách
            }

            return View(logo);
        }

        //// GET: LoGoes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var loGo = await _context.loGos
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (loGo == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView(loGo);
        //}

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVu = await _context.loGos.FindAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }
            return PartialView(dichVu);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LoGo logo, IFormFile imageFile)
        {
            if (id != logo.ID)
            {
                return NotFound();
            }

           /* if (!ModelState.IsValid)*/
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
                    logo.Logo = imageFile.FileName;
                }
                _context.Update(logo);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách
            }

              
            
            return View(logo);
        }

        // GET: LoGoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loGo = await _context.loGos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loGo == null)
            {
                return NotFound();
            }

            return View(loGo);
        }

        // POST: LoGoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loGo = await _context.loGos.FindAsync(id);
            if (loGo != null)
            {
                _context.loGos.Remove(loGo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoGoExists(int id)
        {
            return _context.loGos.Any(e => e.ID == id);
        }
    }
}
