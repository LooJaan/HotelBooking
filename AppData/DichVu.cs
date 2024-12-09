using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
	public class DichVu
	{
		[Key]
		public int ID { get; set; }
		public string Ten { get; set; }
		public string MoTa { get; set; }
		public string Hinh { get; set; }
        public int? LoaiPhongId { get; set; }
		public bool TrangThai { get; set; }
        public ICollection<LoaiPhongDichVu> DichVuLoaiPhongs { get; set; } = new List<LoaiPhongDichVu>();
    }
}
