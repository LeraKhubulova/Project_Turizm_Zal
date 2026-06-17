using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Turizm_Zal.Data;
using Project_Turizm_Zal.Models;
using System;
using System.Threading.Tasks;

namespace Project_Turizm_Zal.Controllers
{
    public class ExhibitsController : Controller
    {
        private readonly MuseumContext _context;

        public ExhibitsController(MuseumContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Details(Guid id)
        {
            var exhibit = await _context.Exhibits
                .Include(e => e.MuseumHall)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exhibit == null) return NotFound();

            return View(exhibit);
        }
    }
}