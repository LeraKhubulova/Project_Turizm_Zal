using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Project_Turizm_Zal.Data;
using Microsoft.AspNetCore.Mvc.Filters;  
using Microsoft.AspNetCore.Http;
using Project_Turizm_Zal.Data;
using Project_Turizm_Zal.Models;
using Project_Turizm_Zal.Services;
using static Project_Turizm_Zal.Models.User;

namespace Project_Turizm_Zal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHallService hallService;
        private readonly IUserService _userService;

        public HomeController(IHallService hallService, IUserService userService)
        {
            this.hallService = hallService;
            _userService = userService;
            this.hallService = hallService;
        }


        public async Task<IActionResult> Hall(Guid id, CancellationToken cancellationToken)
        {
            var hall = await hallService.GetHallById(id, cancellationToken);
            return RedirectToAction("Index", "Hall", hall);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.IsLoggedIn = HttpContext.Session.GetString("UserEmail") != null;
            ViewBag.UserName = HttpContext.Session.GetString("UserName") ?? "";
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Auth()
        {
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.Error = "Заполните все поля";
                return View("Auth", model);
            }

            var user = await _userService.Login(model.Email, model.Password, cancellationToken);

            if (user == null)
            {
                ViewBag.Error = "Неверный email или пароль";
                return View("Auth", model);
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            if (user.Role == UserRole.Admin)
            {
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return Json(new { success = false, message = "Çàïîëíèòå âñå ïîëÿ" });
            }

            if (model.Password != model.ConfirmPassword)
            {
                return Json(new { success = false, message = "Ïàðîëè íå ñîâïàäàþò" });
            }

            if (model.Password.Length < 4)
            {
                return Json(new { success = false, message = "Ïàðîëü äîëæåí áûòü íå ìåíåå 6 ñèìâîëîâ" });
            }

            if (await _userService.IsUserExists(model.Email, cancellationToken))
            {
                return Json(new { success = false, message = "Ïîëüçîâàòåëü ñ òàêèì email óæå ñóùåñòâóåò" });
            }

            var user = new User(model.Name, model.Email, model.Password);

            if (!(await _userService.Register(user, cancellationToken)))
            {
                return Json(new { success = false, message = "Îøèáêà ïðè ðåãèñòðàöèè" });
            }

            return Json(new { success = true });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckAuth()
        {
            var isLoggedIn = HttpContext.Session.GetString("UserEmail") != null;
            return Json(new { isLoggedIn = isLoggedIn });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}