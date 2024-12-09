using Microsoft.AspNetCore.Mvc;
using AppData;
using AppView.ViewModels;
using Microsoft.EntityFrameworkCore;
using static AppView.ViewModels.DetailsRoomVM;

namespace AppView.Controllers
{
    public class RoomController : Controller
    {
        
            private readonly HotelDbContext _context; // Đảm bảo bạn có DbContext của mình

            public RoomController(HotelDbContext context)
            {
                _context = context;
            }

        // GET: LoaiPhong
        public ActionResult Index(int page = 1)
        {
            int pageSize = 5;

            // Lấy danh sách loại phòng với dịch vụ liên kết qua bảng trung gian
            var data = _context.LoaiPhongs
                .Include(lp => lp.DichVuLoaiPhongs)  // Bao gồm bảng trung gian
                .ThenInclude(dvlp => dvlp.DichVu)    // Bao gồm dịch vụ từ bảng trung gian
                .OrderBy(lp => lp.MaLoaiPhong)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(lp => new LoaiPhongVM
                {
                    MaLoaiPhong = lp.MaLoaiPhong,
                    TenLoaiPhong = lp.TenLoaiPhong,
                    MoTa = lp.MoTa,
                    Anh = lp.Anh,
                    GiaGoc = lp.GiaGoc,
                    GiaGiamGia = lp.GiaGiamGia,
                    TrangThai = lp.TrangThai,

                    // Lấy danh sách dịch vụ từ bảng trung gian
                    DichVus = lp.DichVuLoaiPhongs.Select(dvlp => new DichVuVM
                    {
                        Ten = dvlp.DichVu.Ten,  // Lấy thông tin từ bảng DichVu
                        MoTa = dvlp.DichVu.MoTa,
                        Hinh = dvlp.DichVu.Hinh,
                        TrangThai = dvlp.DichVu.TrangThai,
                    }).ToList()
                }).ToList();

            // Lấy tổng số loại phòng để tính số trang
            var totalLoaiPhong = _context.LoaiPhongs.Count();
            var totalPages = (int)Math.Ceiling(totalLoaiPhong / (double)pageSize);

            // Tạo ViewModel phân trang
            var viewModel = new PaginatedLoaiPhongVM
            {
                LoaiPhongs = data,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }


        public IActionResult Details(int id)
        {
            // Lấy loại phòng và hình ảnh liên quan
            var loaiPhong = _context.LoaiPhongs
                                 .Include(lp => lp.HinhAnhPhongs) // Truy vấn bảng AnhChiTiet liên quan
                                 .FirstOrDefault(lp => lp.MaLoaiPhong == id);

            if (loaiPhong == null)
            {
                return NotFound();
            }

            return View(loaiPhong); // Truyền dữ liệu qua View

        }



    }
}
