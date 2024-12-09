using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class ChiTietRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
