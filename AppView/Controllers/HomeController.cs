using AppData;
using AppView.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
       

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Fetch Welcome data
            var responseWelcome = await _httpClient.GetAsync("https://localhost:7097/api/WelComes");
            var jsonDataWelcome = await responseWelcome.Content.ReadAsStringAsync();
            var welcomes = JsonConvert.DeserializeObject<List<WelCome>>(jsonDataWelcome);
        

            // 2. Fetch Slides data
            var responseSlides = await _httpClient.GetAsync("https://localhost:7097/api/Slides");
            var jsonDataSlides = await responseSlides.Content.ReadAsStringAsync();
            var slides = JsonConvert.DeserializeObject<List<Slide>>(jsonDataSlides);
         

            // 3. Fetch MenuItems data
            var responseMenuItems = await _httpClient.GetAsync("https://localhost:7097/api/MenuItems");
            var jsonDataMenuItems = await responseMenuItems.Content.ReadAsStringAsync();
            var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(jsonDataMenuItems);
         

            // 4. Fetch LienHes data
            var responseLienHes = await _httpClient.GetAsync("https://localhost:7097/api/LienHes");
            var jsonDataLienHes = await responseLienHes.Content.ReadAsStringAsync();
            var lienHes = JsonConvert.DeserializeObject<List<LienHe>>(jsonDataLienHes);
           

            // 5. Fetch DichVus data
            var responseDichVus = await _httpClient.GetAsync("https://localhost:7097/api/DichVus");
            var jsonDataDichVus = await responseDichVus.Content.ReadAsStringAsync();
            var dichVus = JsonConvert.DeserializeObject<List<DichVu>>(jsonDataDichVus);
        

            // 6. Fetch LoGos data
            var responseLogo = await _httpClient.GetAsync("https://localhost:7097/api/LoGoes");
            var jsonDataLogo = await responseLogo.Content.ReadAsStringAsync();
            var loGos = JsonConvert.DeserializeObject<List<LoGo>>(jsonDataLogo);
        

            var responseLoaiPhong = await _httpClient.GetAsync("https://localhost:7097/api/LoaiPhongs");
            var jsonDataLoaiPhong = await responseLoaiPhong.Content.ReadAsStringAsync();
            var loaiPhongs = JsonConvert.DeserializeObject<List<LoaiPhong>>(jsonDataLoaiPhong);

            var viewModel = new HomeModel
            {
                DichVus = dichVus,
                LienHes = lienHes,
                Welcomes = welcomes,
                Slides = slides,
                MenuItems = menuItems,
                loGos = loGos,
                LoaiPhongs = loaiPhongs
            };

            return View(viewModel);
        }


    }
}
