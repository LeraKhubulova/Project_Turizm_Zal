using Microsoft.AspNetCore.Mvc;
using Project_Turizm_Zal.Models;
using Project_Turizm_Zal.Services;

namespace Project_Turizm_Zal.Controllers
{
    public class HallController : Controller
    {
        private readonly IHallService hallService;

        public HallController(IHallService hallService)
        {
            this.hallService = hallService;
        }

        public IActionResult Index(MuseumHall hall)
        {
            return View(hall);
        }

        public async Task<IActionResult> Exhibition(Guid id, CancellationToken cancellationToken)
        {
            var exhibition = await hallService.GetExhibitionById(id, cancellationToken);
            return View(exhibition);
        }

        public async Task<IActionResult> Exhibit(Guid id, CancellationToken cancellationToken) {
            var exhibit = await hallService.GetExhibitById(id, cancellationToken);
            var viewModel = new ExhibitViewModel()
            {
                Id = exhibit.Id,
                Name = exhibit.Name,
                Images = exhibit.Images,
                Description = exhibit.Description,
                ExhibitionName = exhibit.Exhibition.Name
            };
            return View(viewModel);
        }

    }
}
