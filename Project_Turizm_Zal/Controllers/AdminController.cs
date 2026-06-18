using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project_Turizm_Zal.Models;
using Project_Turizm_Zal.Models.ViewModels;
using Project_Turizm_Zal.Services;

namespace Project_Turizm_Zal.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHallService _hallService;
        private readonly IFileService _fileService;

        public AdminController(IHallService hallService, IFileService fileService)
        {
            _hallService = hallService;
            _fileService = fileService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role))
            {
                context.Result = RedirectToAction("Auth", "Account");
                return;
            }

            if (role != "Admin")
            {
                context.Result = RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Exhibits(CancellationToken cancellationToken)
        {
            var halls = await _hallService.GetAllHallsWithExhibits(cancellationToken);
            return View(halls);
        }
        [HttpGet]
        public async Task<IActionResult> EditExhibit(Guid id, CancellationToken cancellationToken)
        {
            var exhibit = await _hallService.GetExhibitById(id, cancellationToken);

            if (exhibit == null)
            {
                return NotFound();
            }

            var halls = await _hallService.GetAllHalls(cancellationToken);

            var model = new EditExhibitViewModel
            {
                Id = exhibit.Id,
                MuseumHallId = exhibit.MuseumHallId,
                Halls = halls,
                Name = exhibit.Name,
                ExistingImagePath = exhibit.Images.FirstOrDefault() ?? "",
                Description = exhibit.Description,
                ShortDescription = exhibit.ShortDescription,
                CultureEra = exhibit.CultureEra,
                FindLocation = exhibit.FindLocation,
                Materials = exhibit.Materials,
                Technique = exhibit.Technique,
                Dimensions = exhibit.Dimensions,
                Weight = exhibit.Weight,
                Quantity = exhibit.Quantity,
                Storage = exhibit.Storage,
                Model3DUrl = exhibit.Model3DUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExhibit(EditExhibitViewModel model, CancellationToken cancellationToken)
        {
            if (model.Id == Guid.Empty)
            {
                return NotFound();
            }

            if (model.MuseumHallId == Guid.Empty)
            {
                ViewBag.Error = "Выберите зал";
                model.Halls = await _hallService.GetAllHalls(cancellationToken);
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Description) ||
                string.IsNullOrWhiteSpace(model.ShortDescription))
            {
                ViewBag.Error = "Заполните обязательные поля";
                model.Halls = await _hallService.GetAllHalls(cancellationToken);
                return View(model);
            }

            var imagePath = model.ExistingImagePath;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var newImagePath = await _fileService.SaveImageAsync(
                    model.ImageFile,
                    "exhibits",
                    cancellationToken);

                if (newImagePath == null)
                {
                    ViewBag.Error = "Загрузите изображение экспоната в формате jpg, jpeg, png или webp. Размер файла не должен превышать 5 МБ";
                    model.Halls = await _hallService.GetAllHalls(cancellationToken);
                    return View(model);
                }

                imagePath = newImagePath;
            }

            var exhibit = new Exhibit
            {
                Id = model.Id,
                MuseumHallId = model.MuseumHallId,
                Name = model.Name.Trim(),
                Images = string.IsNullOrWhiteSpace(imagePath)
                    ? new List<string>()
                    : new List<string> { imagePath },
                Description = model.Description.Trim(),
                ShortDescription = model.ShortDescription.Trim(),
                CultureEra = model.CultureEra?.Trim(),
                FindLocation = model.FindLocation?.Trim(),
                Materials = model.Materials?.Trim(),
                Technique = model.Technique?.Trim(),
                Dimensions = model.Dimensions?.Trim(),
                Weight = model.Weight?.Trim(),
                Quantity = model.Quantity?.Trim(),
                Storage = model.Storage?.Trim(),
                Model3DUrl = model.Model3DUrl?.Trim()
            };

            var result = await _hallService.UpdateExhibit(exhibit, cancellationToken);

            if (!result)
            {
                ViewBag.Error = "Не удалось сохранить изменения. Возможно, в выбранном зале уже есть экспонат с таким названием";
                model.Halls = await _hallService.GetAllHalls(cancellationToken);
                return View(model);
            }

            TempData["Success"] = "Экспонат успешно изменен";
            return RedirectToAction("Exhibits");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExhibit(Guid id, CancellationToken cancellationToken)
        {
            var result = await _hallService.DeleteExhibit(id, cancellationToken);

            if (!result)
            {
                TempData["Error"] = "Не удалось удалить экспонат";
                return RedirectToAction("Exhibits");
            }

            TempData["Success"] = "Экспонат удален";
            return RedirectToAction("Exhibits");
        }

        [HttpGet]
        public IActionResult CreateHall()
        {
            return View(new CreateHallViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHall(CreateHallViewModel model, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Description))
            {
                ViewBag.Error = "Заполните обязательные поля";
                return View(model);
            }

            if (model.Number <= 0)
            {
                ViewBag.Error = "Номер зала должен быть больше нуля";
                return View(model);
            }

            var imagePath = await _fileService.SaveImageAsync(
                model.ImageFile,
                "halls",
                cancellationToken);

            if (imagePath == null)
            {
                ViewBag.Error = "Загрузите изображение зала в формате jpg, jpeg, png или webp. Размер файла не должен превышать 5 МБ";
                return View(model);
            }

            var hall = new MuseumHall
            {
                Id = Guid.NewGuid(),
                Name = model.Name.Trim(),
                Number = model.Number,
                Description = model.Description.Trim(),
                ImagePath = imagePath,
                Slogan = model.Slogan?.Trim() ?? "",
                LeftColumnText = model.LeftColumnText?.Trim() ?? "",
                MapData = model.MapData?.Trim() ?? "",
                Exhibits = new List<Exhibit>()
            };

            var result = await _hallService.CreateHall(hall, cancellationToken);

            if (!result)
            {
                ViewBag.Error = "Зал с таким названием или номером уже существует";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddExhibit(CancellationToken cancellationToken)
        {
            var halls = await _hallService.GetAllHalls(cancellationToken);

            var model = new AddExhibitViewModel
            {
                Halls = halls
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExhibit(AddExhibitViewModel model, CancellationToken cancellationToken)
        {
            if (model.MuseumHallId == Guid.Empty)
            {
                ViewBag.Error = "Выберите зал";
                model.Halls = await _hallService.GetAllHalls(cancellationToken);
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Description) ||
                string.IsNullOrWhiteSpace(model.ShortDescription))
            {
                ViewBag.Error = "Заполните обязательные поля";
                model.Halls = await _hallService.GetAllHalls(cancellationToken);
                return View(model);
            }

            var imagePath = await _fileService.SaveImageAsync(
                model.ImageFile,
                "exhibits",
                cancellationToken);

            if (imagePath == null)
            {
                ViewBag.Error = "Загрузите изображение экспоната в формате jpg, jpeg, png или webp. Размер файла не должен превышать 5 МБ";
                model.Halls = await _hallService.GetAllHalls(cancellationToken);
                return View(model);
            }

            var exhibit = new Exhibit(
                model.Name.Trim(),
                new List<string> { imagePath },
                model.Description.Trim(),
                model.MuseumHallId
            )
            {
                ShortDescription = model.ShortDescription.Trim(),
                CultureEra = model.CultureEra?.Trim(),
                FindLocation = model.FindLocation?.Trim(),
                Materials = model.Materials?.Trim(),
                Technique = model.Technique?.Trim(),
                Dimensions = model.Dimensions?.Trim(),
                Weight = model.Weight?.Trim(),
                Quantity = model.Quantity?.Trim(),
                Storage = model.Storage?.Trim(),
                Model3DUrl = model.Model3DUrl?.Trim()
            };

            var result = await _hallService.AddExhibit(exhibit, cancellationToken);

            if (!result)
            {
                ViewBag.Error = "Не удалось добавить экспонат. Возможно, такой экспонат уже есть в выбранном зале";
                model.Halls = await _hallService.GetAllHalls(cancellationToken);
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}