using AppData;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppView.ViewComponents
{
	public class TaiKhoanViewComponent : ViewComponent
	{
		private readonly HotelDbContext db;

		public TaiKhoanViewComponent(HotelDbContext context) => db = context;
		public IViewComponentResult Invoke()
		{
			var data = db.TaiKhoans.Select(x => new TaiKhoanVM
			{
				TaiKhoan = x.TaiKhoan,
				MatKhau = x.MatKhau,
			});
			return View(data);
		}
	}
}
