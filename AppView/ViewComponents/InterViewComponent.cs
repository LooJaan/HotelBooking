using AppData;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppView.ViewComponents
{
    public class InterViewComponent : ViewComponent
    {
        private readonly HotelDbContext db;

        public InterViewComponent(HotelDbContext context) => db = context;
        public IViewComponentResult Invoke()
        {
            var data = db.inters.Select(x => new InterVM
            {
                Hinh = x.Hinh,
                Link = x.Link,
                TrangThai = x.TrangThai,
            });
            return View(data);
        }
    }
}

