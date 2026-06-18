using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project_Turizm_Zal.Models;
using Project_Turizm_Zal.Services;

namespace Project_Turizm_Zal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHallService _hallService;

        public HomeController(IHallService hallService)
        {
            _hallService = hallService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var halls = await _hallService.GetAllHalls(cancellationToken);
            return View(halls);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ViewBag.IsLoggedIn = HttpContext.Session.GetString("UserEmail") != null;
            ViewBag.UserName = HttpContext.Session.GetString("UserName") ?? "";
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole") ?? "";
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutTeam()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}