using AppData;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppView.ViewComponents
{
    public class LienHeViewComponent : ViewComponent
    {
        private readonly HotelDbContext db;

        public LienHeViewComponent(HotelDbContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.lienHes.Select(x => new LienHeVM
            {
                SoDienThoai = x.SoDienThoai,
                LogoSDT = x.LogoSDT,
            });
            return View(data);
        }
    }


}
