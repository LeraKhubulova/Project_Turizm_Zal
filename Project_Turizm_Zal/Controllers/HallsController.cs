using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Turizm_Zal.Data;
using Project_Turizm_Zal.Models;
using System;
using System.Threading.Tasks;

namespace Project_Turizm_Zal.Controllers
{
    public class HallsController : Controller
    {
        private readonly MuseumContext _context;

        public HallsController(MuseumContext context)
        {
            _context = context;
        }



        //https://localhost:7075/Halls/SeedHalls
        //https://localhost:7075/Halls/SeedExhibits


        public async Task<IActionResult> SeedHalls()
        {
            // 1. Египетский зал 
            var egypt = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "Египетский зал");
            if (egypt == null)
            {
                egypt = new MuseumHall();
                _context.Halls.Add(egypt);
            }

            egypt.Name = "Египетский зал";
            egypt.Number = 1;
            egypt.Description = "Зал древнеегипетских экспонатов. Здесь вы увидите саркофаги, папирусы и статуи фараонов.";
            egypt.ImagePath = "/images/TestHalls/TestHallEgupt.png";
            egypt.Slogan = "Загадки фараонов и сокровища пирамид";
            egypt.LeftColumnText = "Древний Египет оставил нам множество тайн. В этом зале вы узнаете о мумификации, иероглифах и божествах.";
            egypt.MapData = System.Text.Json.JsonSerializer.Serialize(new
            {
                centerLat = 26.8206,
                centerLng = 30.8025,
                zoom = 7,
                markers = new[]
                {
                    new { lat = 29.9765, lng = 31.1313, popup = "<b>Гиза</b><br>Менкаура и царица" },
                    new { lat = 27.645, lng = 30.925, popup = "<b>Амарна</b><br>Бюст Нефертити" },
                    new { lat = 25.74, lng = 32.605, popup = "<b>Фивы</b><br>Статуя Сехмет" },
                    new { lat = 29.844, lng = 31.254, popup = "<b>Мемфис</b><br>Колосс Рамсеса II" }
                }
            });

            // 2. Зал Картин 
            var painting = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "Зал Картин");
            if (painting == null)
            {
                painting = new MuseumHall();
                _context.Halls.Add(painting);
            }

            painting.Name = "Зал Картин";
            painting.Number = 2;
            painting.Description = "Шедевры живописи разных эпох. От Ренессанса до авангарда.";
            painting.ImagePath = "/images/index/prob.jpg";
            painting.Slogan = "Искусство, вдохновляющее поколения";
            painting.LeftColumnText = "В этом зале собраны картины великих мастеров. Погрузитесь в мир красок и эмоций.";
            painting.MapData = System.Text.Json.JsonSerializer.Serialize(new
            {
                centerLat = 48.8566,
                centerLng = 2.3522,
                zoom = 5,
                markers = new[]
                {
                    new { lat = 48.8606, lng = 2.3376, popup = "<b>Лувр</b><br>Мона Лиза" },
                    new { lat = 41.9028, lng = 12.4964, popup = "<b>Ватикан</b><br>Сикстинская капелла" }
                }
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SeedExhibits()
        {
            var egypt = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "Египетский зал");
            var painting = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "Зал Картин");

            // бюст
            var exhibit1 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Бюст Нефертити");
            if (exhibit1 == null)
            {
                exhibit1 = new Exhibit(
                    "Бюст Нефертити",
                    new List<string> { "/images/exhibits/nefertiti.jpg" },
                    "Легендарный бюст царицы, символ красоты Древнего Египта",
                    egypt.Id
                );
                _context.Exhibits.Add(exhibit1);
            }
            else
            {
                exhibit1.Name = "Бюст Нефертити";
                exhibit1.Images = new List<string> { "/images/exhibits/nefertiti.jpg" };
                exhibit1.Description = "Легендарный бюст царицы, символ красоты Древнего Египта";
                exhibit1.MuseumHallId = egypt.Id;
                _context.Exhibits.Update(exhibit1);
            }

            // колос
            var exhibit2 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Колосс Рамсеса II");
            if (exhibit2 == null)
            {
                exhibit2 = new Exhibit(
                    "Колосс Рамсеса II",
                    new List<string> { "/images/exhibits/ramses.jpg" },
                    "Огромная статуя великого фараона Рамсеса II",
                    egypt.Id
                );
                _context.Exhibits.Add(exhibit2);
            }
            else
            {
                exhibit2.Name = "Колосс Рамсеса II";
                exhibit2.Images = new List<string> { "/images/exhibits/ramses.jpg" };
                exhibit2.Description = "Огромная статуя великого фараона Рамсеса II";
                exhibit2.MuseumHallId = egypt.Id;
                _context.Exhibits.Update(exhibit2);
            }

            // сехмет 
            var exhibit3 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Статуя богини Сехмет");
            if (exhibit3 == null)
            {
                exhibit3 = new Exhibit(
                    "Статуя богини Сехмет",
                    new List<string> { "/images/exhibits/sekhmet.jpg" },
                    "Львиноголовая богиня войны и исцеления",
                    egypt.Id
                );
                _context.Exhibits.Add(exhibit3);
            }
            else
            {
                exhibit3.Name = "Статуя богини Сехмет";
                exhibit3.Images = new List<string> { "/images/exhibits/sekhmet.jpg" };
                exhibit3.Description = "Львиноголовая богиня войны и исцеления";
                exhibit3.MuseumHallId = egypt.Id;
                _context.Exhibits.Update(exhibit3);
            }

            // монолиза
            var exhibit4 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Мона Лиза");
            if (exhibit4 == null)
            {
                exhibit4 = new Exhibit(
                    "Мона Лиза",
                    new List<string> { "/images/exhibits/mona_lisa.jpg" },
                    "Шедевр Леонардо да Винчи, известный своей загадочной улыбкой",
                    painting.Id
                );
                _context.Exhibits.Add(exhibit4);
            }
            else
            {
                exhibit4.Name = "Мона Лиза";
                exhibit4.Images = new List<string> { "/images/exhibits/mona_lisa.jpg" };
                exhibit4.Description = "Шедевр Леонардо да Винчи, известный своей загадочной улыбкой";
                exhibit4.MuseumHallId = painting.Id;
                _context.Exhibits.Update(exhibit4);
            }

            // звездная ночь 
            var exhibit5 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Звёздная ночь");
            if (exhibit5 == null)
            {
                exhibit5 = new Exhibit(
                    "Звёздная ночь",
                    new List<string> { "/images/exhibits/starry_night.jpg" },
                    "Знаменитая картина Винсента Ван Гога",
                    painting.Id
                );
                _context.Exhibits.Add(exhibit5);
            }
            else
            {
                exhibit5.Name = "Звёздная ночь";
                exhibit5.Images = new List<string> { "/images/exhibits/starry_night.jpg" };
                exhibit5.Description = "Знаменитая картина Винсента Ван Гога";
                exhibit5.MuseumHallId = painting.Id;
                _context.Exhibits.Update(exhibit5);
            }


           
           

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var hall = await _context.Halls
                 .Include(h => h.Exhibits)
                 .FirstOrDefaultAsync(h => h.Id == id);
            if (hall == null) return NotFound();
            return View(hall);
        }
    }
}