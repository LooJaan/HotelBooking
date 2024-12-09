using AppData;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppView.ViewComponents
{
    public class GGViewComponent : ViewComponent
    {
        private readonly HotelDbContext db;

        public GGViewComponent(HotelDbContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.gGs.Select(x => new GGVM
            {
                Hinh = x.Hinh,
                Link = x.Link,
                TrangThai = x.TrangThai,
            });
            return View(data);
        }
    }
}
