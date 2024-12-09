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
    public class LoaiPhongsController : Controller
    {
        private readonly HotelDbContext _context;

        public LoaiPhongsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: LoaiPhongs
        public async Task<IActionResult> Index()
        {
            return PartialView(await _context.LoaiPhongs.ToListAsync());
        }

        // GET: LoaiPhongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhong = await _context.LoaiPhongs
                .FirstOrDefaultAsync(m => m.MaLoaiPhong == id);
            if (loaiPhong == null)
            {
                return NotFound();
            }

            return PartialView(loaiPhong);
        }

        // GET: LoaiPhongs/Create
         public IActionResult Create()
        {
            return PartialView();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoaiPhong,TenLoaiPhong,MoTa,Anh,GiaGoc,Giuong,Size,GiaGiamGia,TrangThai")] LoaiPhong loaiPhong, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Lưu file vào thư mục trên server
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/room", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn file vào database
                    loaiPhong.Anh = imageFile.FileName;
                }

                _context.Add(loaiPhong);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách
            }

            return View(loaiPhong);
        }

        // GET: LoaiPhongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhong = await _context.LoaiPhongs.FindAsync(id);
            if (loaiPhong == null)
            {
                return NotFound();
            }
            return PartialView(loaiPhong);
        }

        // POST: LoaiPhongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiPhong,TenLoaiPhong,MoTa,Anh,GiaGoc,Giuong,Size,GiaGiamGia,TrangThai")] LoaiPhong loaiPhong, IFormFile imageFile)
        {
            if (id != loaiPhong.MaLoaiPhong)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Lưu file vào thư mục trên server
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/room", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn file vào database
                    loaiPhong.Anh = imageFile.FileName;
                }

                _context.Update(loaiPhong);  // Thêm đối tượng DichVu vào CSDL
                _context.SaveChanges();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách
            }

            return View(loaiPhong);
        }

        // GET: LoaiPhongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhong = await _context.LoaiPhongs
                .FirstOrDefaultAsync(m => m.MaLoaiPhong == id);
            if (loaiPhong == null)
            {
                return NotFound();
            }

            return View(loaiPhong);
        }

        // POST: LoaiPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiPhong = await _context.LoaiPhongs.FindAsync(id);
            if (loaiPhong != null)
            {
                _context.LoaiPhongs.Remove(loaiPhong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiPhongExists(int id)
        {
            return _context.LoaiPhongs.Any(e => e.MaLoaiPhong == id);
        }
    }
}
