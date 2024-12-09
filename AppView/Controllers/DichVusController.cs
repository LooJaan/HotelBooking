using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AppView.Controllers
{
    public class DichVusController : Controller
    {
        private readonly HotelDbContext _context;

        public DichVusController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/DichVu
        public async Task<IActionResult> Index()
        {
            var dichVus = await _context.DichVus.ToListAsync();
            return PartialView(dichVus);
        }

        // GET: DichVu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dichVu == null)
            {
                return NotFound();
            }

            return PartialView(dichVu);
        }

        // GET: DichVu/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: DichVu/Create
        [HttpPost]


        [HttpPost]
        public async Task<IActionResult> Create([Bind("ID,Ten,MoTa,Hinh,LoaiPhongId,TrangThai")] DichVu dichVu, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra nếu có hình ảnh được tải lên
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Đường dẫn lưu tệp
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/icon", imageFile.FileName);

                    // Lưu tệp vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Cập nhật đường dẫn hình ảnh vào thuộc tính Hinh
                    dichVu.Hinh = imageFile.FileName;
                }

                _context.Add(dichVu);  // Thêm đối tượng DichVu vào CSDL
                await _context.SaveChangesAsync();  // Lưu thay đổi vào CSDL
                return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách
            }

            return View(dichVu);  // Nếu có lỗi, trả về lại form với thông báo lỗi
        }



        // GET: DichVu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVus.FindAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }
            return PartialView(dichVu);
        }

        // POST: DichVu/Edit/5
        [HttpPost]
       
        public async Task<IActionResult> Edit(int id,  DichVu dichVu, IFormFile imageFile)
        {
            if (id != dichVu.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    // Nếu có hình ảnh mới, lưu hình ảnh và cập nhật đường dẫn
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Đường dẫn lưu tệp
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/icon", imageFile.FileName);

                        // Lưu tệp vào thư mục
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        // Cập nhật đường dẫn hình ảnh vào thuộc tính Hinh
                        dichVu.Hinh = imageFile.FileName;
                    }

                    _context.Update(dichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DichVuExists(dichVu.ID))
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
            return View(dichVu);
        }

        // GET: DichVu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }

        // POST: DichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dichVu = await _context.DichVus.FindAsync(id);
            _context.DichVus.Remove(dichVu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DichVuExists(int id)
        {
            return _context.DichVus.Any(e => e.ID == id);
        }
    }
}
