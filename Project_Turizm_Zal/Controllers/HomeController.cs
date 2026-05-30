using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;  
using Microsoft.AspNetCore.Http;
using Project_Turizm_Zal.Models;
using Project_Turizm_Zal.Services;

namespace Project_Turizm_Zal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
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
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return Json(new { success = false, message = "Заполните все поля" });
            }

            var user = _userService.Login(model.Email, model.Password);
            if (user == null)
            {
                return Json(new { success = false, message = "Неверный email или пароль" });
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            return Json(new { success = true, userName = user.Name });
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return Json(new { success = false, message = "Заполните все поля" });
            }

            if (model.Password != model.ConfirmPassword)
            {
                return Json(new { success = false, message = "Пароли не совпадают" });
            }

            if (model.Password.Length < 6)
            {
                return Json(new { success = false, message = "Пароль должен быть не менее 6 символов" });
            }

            if (_userService.IsUserExists(model.Email))
            {
                return Json(new { success = false, message = "Пользователь с таким email уже существует" });
            }

            var user = new User(model.Name, model.Email, model.Password);

            if (!_userService.Register(user))
            {
                return Json(new { success = false, message = "Ошибка при регистрации" });
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