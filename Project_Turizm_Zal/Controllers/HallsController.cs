using Microsoft.AspNetCore.Mvc;
using Project_Turizm_Zal.Services;

namespace Project_Turizm_Zal.Controllers
{
    public class HallsController : Controller
    {
        private readonly IHallService _hallService;

        public HallsController(IHallService hallService)
        {
            _hallService = hallService;
        }

        public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
        {
            var hall = await _hallService.GetHallWithExhibits(id, cancellationToken);

            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }
    }
}