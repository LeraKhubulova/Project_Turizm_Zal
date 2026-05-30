using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Project_Turizm_Zal.Data;
using Project_Turizm_Zal.Models;
using Project_Turizm_Zal.Services;

namespace Project_Turizm_Zal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHallService hallService;

        public HomeController(IHallService hallService)
        {
            this.hallService = hallService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Hall(Guid id, CancellationToken cancellationToken)
        {
            var hall = hallService.GetHallById(id, cancellationToken).Result;
            return RedirectToAction("Index", "Hall", hall);
        }
    }
}
