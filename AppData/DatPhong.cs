using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
    using System.ComponentModel.DataAnnotations;

    public class DatPhong
    {
        public DatPhong()
        {
            NgayDat = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        public int? LoaiPhongID { get; set; }

        [Required(ErrorMessage = "Khách hàng không được để trống.")]
        public string? KhachHang { get; set; }

        [RegularExpression(@"^\d{12}$", ErrorMessage = "Căn cước công dân phải chứa đúng 12 chữ số.")]
        public string? CCCD { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^(\+84|0)\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? SoDienThoai { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số người lớn phải lớn hơn 0.")]
        public int? SoNguoiLon { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số trẻ em phải là số không âm.")]
        public int? SoTreEm { get; set; }

        [Required(ErrorMessage = "Ngày đặt phòng không được để trống.")]
        public DateTime NgayDat { get; set; }

        [Required(ErrorMessage = "Ngày nhận phòng không được để trống.")]
        [CustomValidation(typeof(DatPhong), nameof(ValidateNgayNhan))]
        public DateTime NgayNhan { get; set; }

        [Required(ErrorMessage = "Ngày trả phòng không được để trống.")]
        [CustomValidation(typeof(DatPhong), nameof(ValidateNgayTra))]
        public DateTime NgayTra { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phòng phải lớn hơn 0.")]
        public int? SoLuongPhong { get; set; }

        public string? GhiChu { get; set; }

        public LoaiPhong? LoaiPhong { get; set; }
        

        // Custom validation methods
        public static ValidationResult? ValidateNgayNhan(DateTime ngayNhan, ValidationContext context)
        {
            var instance = (DatPhong)context.ObjectInstance;

            if (ngayNhan < instance.NgayDat)
            {
                return new ValidationResult("Ngày nhận không được nhỏ hơn ngày đặt.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateNgayTra(DateTime ngayTra, ValidationContext context)
        {
            var instance = (DatPhong)context.ObjectInstance;

            if (ngayTra < instance.NgayDat)
            {
                return new ValidationResult("Ngày trả không được nhỏ hơn ngày đặt.");
            }

            if (ngayTra < instance.NgayNhan)
            {
                return new ValidationResult("Ngày trả không được nhỏ hơn ngày nhận.");
            }

            return ValidationResult.Success;
        }

    }

}
