using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
    public class AnhChiTiet
    {
        public int Id { get; set; }
        public string? Anh { get; set; }
        public bool TrangThai { get; set; }
        public int? IdLoaiPhong { get; set; }
        public LoaiPhong? LoaiPhong { get; set; }
     
    }
}
