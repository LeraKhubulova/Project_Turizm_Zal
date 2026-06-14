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
            var egypt = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "ЗАЛ ДРЕВНЕГО ЕГИПТА");
            if (egypt == null)
            {
                egypt = new MuseumHall();
                _context.Halls.Add(egypt);
            }

            egypt.Name = "ЗАЛ ДРЕВНЕГО ЕГИПТА";
            egypt.Number = 1;
            egypt.Slogan = "𓋴 𓂧 𓈖𓃀 𓅓𓃀𓂧𓅱";
            egypt.Description = "Древний Египет — цивилизация фараонов, пирамид и загадок, существовавшая более трёх тысячелетий.";
            egypt.ImagePath = "/images/TestHalls/TestHallEgupt.png";
            egypt.LeftColumnText = "На протяжении трёх тысячелетий страна сохраняла культурное единство: фараоны считались живыми богами, строились пирамиды, храмы и создавались шедевры скульптуры.<br><br>Эпоха Древнего царства (ок. 2686–2181 гг. до н.э.) — время великих пирамид в Гизе и Дахшуре. Среднее царство (2055–1650 гг. до н.э.) принесло расцвет литературы и искусства. Новое царство (1550–1069 гг. до н.э.) стало «золотым веком»: именно тогда жили Эхнатон, Нефертити, Тутанхамон и Рамсес II. Поздний период завершился завоеванием Египта Александром Македонским в 332 г. до н.э., а затем римским владычеством (30 г. до н.э.).<br><br>На карте отмечены ключевые археологические зоны: Гиза (статуя фараона Менкаура и царицы), Амарна (бюст Нефертити), Фивы/Луксор (Статуя богини Сехмет) и Мемфис (колосс Рамсеса II). Каждый маркер связан с экспонатами нашей коллекции.";
            egypt.MapData = System.Text.Json.JsonSerializer.Serialize(new
            {
                centerLat = 26.8206,
                centerLng = 30.8025,
                zoom = 7,
                markers = new[]
                {
                    new { lat = 29.9765, lng = 31.1313, popup = "<b>Гиза</b><br>Менкаура и царица" },
                    new { lat = 27.645, lng = 30.925, popup = "<b>Амарна</b><br>Бюст Нефертити" },
                    new { lat = 25.74, lng = 32.605, popup = "<b>Фивы</b><br>Статуя богини Сехмет" },
                    new { lat = 29.844, lng = 31.254, popup = "<b>Мемфис</b><br>Колосс Рамсеса II" }
            }
            });

            // 2. Зал Картин 
            var painting = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "ЗАЛ КАРТИН");
            if (painting == null)
            {
                painting = new MuseumHall();
                _context.Halls.Add(painting);
            }

            painting.Name = "ЗАЛ КАРТИН";
            painting.Number = 2;
            painting.Slogan = "Искусство, вдохновляющее поколения";
            painting.Description = "Шедевры живописи разных эпох. От Ренессанса до авангарда.";
            painting.ImagePath = "/images/index/prob.jpg";
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
            var egypt = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "ЗАЛ ДРЕВНЕГО ЕГИПТА");
            var painting = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "ЗАЛ КАРТИН");

            // 1. Менкаура и царица (Гиза)
            var exhibit1 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Менкаура и царица");
            if (exhibit1 == null)
            {
                exhibit1 = new Exhibit(
                    "Менкаура и царица",
                    new List<string> { "/images/exhibits/egupt/menkaure.png" },
                    "Статуя фараона Менкаура и царицы из серовато-зелёного сланца. Шедевр скульптуры эпохи Древнего царства.",
                    egypt.Id
                );
                _context.Exhibits.Add(exhibit1);
            }
            else
            {
                exhibit1.Name = "Менкаура и царица";
                exhibit1.Images = new List<string> { "/images/exhibits/egupt/menkaure.png" };
                exhibit1.Description = "Статуя фараона Менкаура и царицы из серовато-зелёного сланца. Шедевр скульптуры эпохи Древнего царства.";
                exhibit1.MuseumHallId = egypt.Id;
                _context.Exhibits.Update(exhibit1);
            }

            // 2. Бюст Нефертити (Амарна)
            var exhibit2 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Бюст Нефертити");
            if (exhibit2 == null)
            {
                exhibit2 = new Exhibit(
                    "Бюст Нефертити",
                    new List<string> { "/images/exhibits/egupt/nefertiti.png" },
                    "Легендарный бюст царицы Нефертити, символ красоты Древнего Египта. Найден в Амарне.",
                    egypt.Id
                );
                _context.Exhibits.Add(exhibit2);
            }
            else
            {
                exhibit2.Name = "Бюст Нефертити";
                exhibit2.Images = new List<string> { "/images/exhibits/egupt/nefertiti.png" };
                exhibit2.Description = "Легендарный бюст царицы Нефертити, символ красоты Древнего Египта. Найден в Амарне.";
                exhibit2.MuseumHallId = egypt.Id;
                _context.Exhibits.Update(exhibit2);
            }

            // 3. Статуя богини Сехмет (Фивы)
            var exhibit3 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Статуя богини Сехмет");
            if (exhibit3 == null)
            {
                exhibit3 = new Exhibit(
                    "Статуя богини Сехмет",
                    new List<string> { "/images/exhibits/egupt/sekhmet.png" },
                    "Львиноголовая богиня войны и исцеления. Статуя из чёрного гранита, найденная в Фивах.",
                    egypt.Id
                );
                _context.Exhibits.Add(exhibit3);
            }
            else
            {
                exhibit3.Name = "Статуя богини Сехмет";
                exhibit3.Images = new List<string> { "/images/exhibits/egupt/sekhmet.png" };
                exhibit3.Description = "Львиноголовая богиня войны и исцеления. Статуя из чёрного гранита, найденная в Фивах.";
                exhibit3.MuseumHallId = egypt.Id;
                _context.Exhibits.Update(exhibit3);
            }

            // 4. Колосс Рамсеса II (Мемфис)
            var exhibit4 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Колосс Рамсеса II");
            if (exhibit4 == null)
            {
                exhibit4 = new Exhibit(
                    "Колосс Рамсеса II",
                    new List<string> { "/images/exhibits/egupt/ramses.png" },
                    "Огромная известняковая статуя фараона Рамсеса II из Мемфиса. Символ военной мощи Египта.",
                    egypt.Id
                );
                _context.Exhibits.Add(exhibit4);
            }
            else
            {
                exhibit4.Name = "Колосс Рамсеса II";
                exhibit4.Images = new List<string> { "/images/exhibits/egupt/ramses.png" };
                exhibit4.Description = "Огромная известняковая статуя фараона Рамсеса II из Мемфиса. Символ военной мощи Египта.";
                exhibit4.MuseumHallId = egypt.Id;
                _context.Exhibits.Update(exhibit4);
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