using AppData;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppView.ViewComponents
{
	public class TinTucViewComponent : ViewComponent
	{
		private readonly HotelDbContext db;

		public TinTucViewComponent(HotelDbContext context) => db = context;
		public IViewComponentResult Invoke()
		{
			var data = db.tinTucs.Select(content => new TinTucVM
			{
				TenTinTucChinh = content.TenTinTucChinh,
				TenTinTucPhu = content.TenTinTucPhu,
				NoiDungNgan = content.NoiDungNgan,
				NoiDungChiTiet1 = content.NoiDungChiTiet1,
				NoiDungChiTiet2 = content.NoiDungChiTiet2,
				NoiDungChiTiet3 = content.NoiDungChiTiet3,
				NoiDungChiTiet4 = content.NoiDungChiTiet4,
				HinhAnh1 = content.HinhAnh1,
				HinhAnh2 = content.HinhAnh2,
				HinhAnh3 = content.HinhAnh3,
				TrangThai = content.TrangThai,
			}).ToList();
			return View(data);
		}
	}
}
