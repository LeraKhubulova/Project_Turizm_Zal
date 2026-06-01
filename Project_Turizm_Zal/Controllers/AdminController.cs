using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Turizm_Zal.Services;

namespace Project_Turizm_Zal.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHallService hallService;

        public AdminController(IHallService hallService)
        {
            this.hallService = hallService;
        }

        public IActionResult Index()
        {
            return View();
        }



    }
}
