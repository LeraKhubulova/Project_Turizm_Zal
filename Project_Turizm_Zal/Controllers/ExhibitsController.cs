using Microsoft.AspNetCore.Mvc;
using Project_Turizm_Zal.Services;

namespace Project_Turizm_Zal.Controllers
{
    public class ExhibitsController : Controller
    {
        private readonly IHallService _hallService;

        public ExhibitsController(IHallService hallService)
        {
            _hallService = hallService;
        }

        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
        {
            var exhibit = await _hallService.GetExhibitWithHall(id, cancellationToken);

            if (exhibit == null)
            {
                return NotFound();
            }

            return View(exhibit);
        }
    }
}