using Microsoft.AspNetCore.Mvc;
using Project_Turizm_Zal.Models;
using Project_Turizm_Zal.Services;
using static Project_Turizm_Zal.Models.User;

namespace Project_Turizm_Zal.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Auth(string tab = "Login")
        {
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ActiveTab = tab == "Register" ? "Register" : "Login";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, CancellationToken cancellationToken)
        {
            ViewBag.ActiveTab = "Login";

            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                ViewBag.LoginError = "Заполните все поля";
                return View("Auth", model);
            }

            var user = await _userService.Login(model.Email.Trim(), model.Password, cancellationToken);

            if (user == null)
            {
                ViewBag.LoginError = "Неверный email или пароль";
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

            HttpContext.Session.SetString("UserRole", "User");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, CancellationToken cancellationToken)
        {
            ViewBag.ActiveTab = "Register";

            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password))
            {
                ViewBag.RegisterError = "Заполните все поля";
                return View("Auth", model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.RegisterError = "Пароли не совпадают";
                return View("Auth", model);
            }

            if (model.Password.Length < 5)
            {
                ViewBag.RegisterError = "Пароль должен быть не менее 6 символов";
                return View("Auth", model);
            }

            if (await _userService.IsUserExists(model.Email.Trim(), cancellationToken))
            {
                ViewBag.RegisterError = "Пользователь с таким email уже существует";
                return View("Auth", model);
            }

            var user = new User(model.Name.Trim(), model.Email.Trim(), model.Password);

            var result = await _userService.Register(user, cancellationToken);

            if (!result)
            {
                ViewBag.RegisterError = "Ошибка при регистрации";
                return View("Auth", model);
            }

            ViewBag.RegisterSuccess = "Регистрация прошла успешно. Теперь вы можете войти.";
            ViewBag.ActiveTab = "Login";

            return View("Auth");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CheckAuth()
        {
            var isLoggedIn = HttpContext.Session.GetString("UserEmail") != null;
            return Json(new { isLoggedIn });
        }
    }
}   