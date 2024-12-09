using System.ComponentModel.DataAnnotations;

namespace AppView.ViewModels
{
	public class TaiKhoanVM
	{
		[Key]
		public int ID { get; set; }
		public string TaiKhoan { get; set; }
		public string MatKhau { get; set; }
	}
}
