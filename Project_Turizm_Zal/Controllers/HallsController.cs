using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Project_Turizm_Zal.Data;                     
using Project_Turizm_Zal.Models;

namespace Project_Turizm_Zal.Controllers
{
    public class HallsController : Controller
    {
        private readonly MuseumContext _context;     

        public HallsController(MuseumContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> TestHall()
        {
            var hall = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "Египетский зал");
            if (hall == null)
            {
                hall = new MuseumHall
                {
                    Name = "Египетский зал",
                    Number = 1,
                    Description = "Зал древнеегипетских экспонатов",
                    ImagePath = "/images/TestHalls/TestHallEgupt.png"
                };
                _context.Halls.Add(hall);
                await _context.SaveChangesAsync();
            }
            else if (string.IsNullOrEmpty(hall.ImagePath))
            {
                hall.ImagePath = "/images/TestHalls/TestHallEgupt.png";
                _context.Halls.Update(hall);
                await _context.SaveChangesAsync();
            }

            return View(hall);
        }
    }
}
