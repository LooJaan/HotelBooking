using AppData;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppView.ViewComponents
{
    public class TikTokViewComponent : ViewComponent
    {
        private readonly HotelDbContext db;

        public TikTokViewComponent(HotelDbContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.tikTok.Select(x => new TikTokVM
            {
                Hinh = x.Hinh,
                Link = x.Link,
                TrangThai = x.TrangThai,
            });
            return View(data);
        }
    }
}
