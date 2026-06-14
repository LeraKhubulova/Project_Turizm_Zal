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
        //это ссылки для быстрого копирования и вызова этих методов

        public async Task<IActionResult> SeedHalls()//для залов
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

            // 2. Кочующие империи Евразии
            var steppe = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "КОЧУЮЩИЕ ИМПЕРИИ ЕВРАЗИИ");
            if (steppe == null)
            {
                steppe = new MuseumHall();
                _context.Halls.Add(steppe);
            }

            steppe.Name = "КОЧУЮЩАЯ ИМПЕРИЯ";
            steppe.Number = 2;
            steppe.Slogan = "Скифы, сарматы, аланы — кочевники, изменившие историю Евразии";
            steppe.Description = "Скифы, сарматы и аланы — три великие кочевые культуры, господствовавшие в евразийских степях более тысячи лет.";
            steppe.ImagePath = "/images/TestHalls/Alans.png";
            steppe.LeftColumnText = "Скифы (VII–III вв. до н.э.) — первые цари степей, создатели «звериного стиля». Их политическое ядро — Приднепровье (современная Украина). Царский курган Толстая Могила подарил миру знаменитую золотую пектораль — шедевр ювелирного искусства, отражающий скифскую космогонию. Женские погребения содержат бронзовые зеркала с фигурками животных — ритуальные «окна» в иной мир.<br><br>Сарматы (III в. до н.э. – IV в. н.э.) пришли с востока, из степей Южного Урала и Поволжья. В курганах Оренбуржья найден парадный меч-акинак с перекрестием «бабочка» — знак военной аристократии и культа бога войны. Пастовые бусы из египетского стекла, найденные на запястье девушки из приволжского могильника, свидетельствуют о широких торговых связях и высоком статусе.<br><br>Аланы — наследники сарматов, в эпоху Великого переселения народов дошли до Испании, а на Кавказе создали христианское Аланское царство. Представленные реликвии — голоса исчезнувшего мира, объединившего степи от Карпат до Каспия.";
            steppe.MapData = System.Text.Json.JsonSerializer.Serialize(new
            {
                centerLat = 45.0,
                centerLng = 50.0,
                zoom = 4,
                markers = new[]
                {
                    new { lat = 48.0, lng = 34.0, popup = "<b>Толстая Могила</b><br>Золотая пектораль" },
                    new { lat = 46.0, lng = 32.0, popup = "<b>Скифские курганы</b><br>Бронзовое зеркало" },
                    new { lat = 51.0, lng = 55.0, popup = "<b>Южный Урал</b><br>Парадный меч-акинак" },
                    new { lat = 48.0, lng = 46.0, popup = "<b>Поволжье</b><br>Пастовая бусина" }
                }
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SeedExhibits()//для экспонатов
        {
            var egypt = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "ЗАЛ ДРЕВНЕГО ЕГИПТА");
            var steppe = await _context.Halls.FirstOrDefaultAsync(h => h.Name == "КОЧУЮЩАЯ ИМПЕРИЯ");

            // 1. Менкаура и царица Гиза
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

            // 2. Бюст Нефертити 
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

            // 3. Статуя богини Сехмет 
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

            // 4. Колосс Рамсеса II 
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

            // ------ КОЧУЮЩАЯ ИМПЕРИЯ-----------------------------------------------------------------------------------------------------------
            

            // 1. Золотая пектораль
            var steppeExhibit1 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Золотая пектораль");
            if (steppeExhibit1 == null)
            {
                steppeExhibit1 = new Exhibit(
                    "Золотая пектораль",
                    new List<string> { "/images/exhibits/steppe/pectoral.png" },
                    "Царское нагрудное украшение-миф. Сцены быта, степного космоса и борьбы чудовищ. Шедевр мирового ювелирного искусства.",
                    steppe.Id
                );
                _context.Exhibits.Add(steppeExhibit1);
            }
            else
            {
                steppeExhibit1.Name = "Золотая пектораль";
                steppeExhibit1.Images = new List<string> { "/images/exhibits/steppe/pectoral.png" };
                steppeExhibit1.Description = "Царское нагрудное украшение-миф. Сцены быта, степного космоса и борьбы чудовищ. Шедевр мирового ювелирного искусства.";
                steppeExhibit1.MuseumHallId = steppe.Id;
                _context.Exhibits.Update(steppeExhibit1);
            }

            // 2. Бронзовое зеркало 
            var steppeExhibit2 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Бронзовое зеркало");
            if (steppeExhibit2 == null)
            {
                steppeExhibit2 = new Exhibit(
                    "Бронзовое зеркало",
                    new List<string> { "/images/exhibits/steppe/mirror.png" },
                    "Бронзовое зеркало с фигурками животных. Ритуальный предмет, открывавший путь в иной мир.",
                    steppe.Id
                );
                _context.Exhibits.Add(steppeExhibit2);
            }
            else
            {
                steppeExhibit2.Name = "Бронзовое зеркало";
                steppeExhibit2.Images = new List<string> { "/images/exhibits/steppe/mirror.png" };
                steppeExhibit2.Description = "Бронзовое зеркало с фигурками животных. Ритуальный предмет, открывавший путь в иной мир.";
                steppeExhibit2.MuseumHallId = steppe.Id;
                _context.Exhibits.Update(steppeExhibit2);
            }

            // 3. Парадный меч-акинак 
            var steppeExhibit3 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Парадный меч-акинак");
            if (steppeExhibit3 == null)
            {
                steppeExhibit3 = new Exhibit(
                    "Парадный меч-акинак",
                    new List<string> { "/images/exhibits/steppe/akinak.png" },
                    "Парадный меч с перекрестием «бабочка». Знак военной аристократии и культа бога войны.",
                    steppe.Id
                );
                _context.Exhibits.Add(steppeExhibit3);
            }
            else
            {
                steppeExhibit3.Name = "Парадный меч-акинак";
                steppeExhibit3.Images = new List<string> { "/images/exhibits/steppe/akinak.png" };
                steppeExhibit3.Description = "Парадный меч с перекрестием «бабочка». Знак военной аристократии и культа бога войны.";
                steppeExhibit3.MuseumHallId = steppe.Id;
                _context.Exhibits.Update(steppeExhibit3);
            }

            // 4. Пастовая бусина 
            var steppeExhibit4 = await _context.Exhibits.FirstOrDefaultAsync(e => e.Name == "Пастовая бусина");
            if (steppeExhibit4 == null)
            {
                steppeExhibit4 = new Exhibit(
                    "Пастовая бусина",
                    new List<string> { "/images/exhibits/steppe/bead.png" },
                    "Крупная пастовая бусина из египетского стекла. Свидетельство широких торговых связей и высокого статуса.",
                    steppe.Id
                );
                _context.Exhibits.Add(steppeExhibit4);
            }
            else
            {
                steppeExhibit4.Name = "Пастовая бусина";
                steppeExhibit4.Images = new List<string> { "/images/exhibits/steppe/bead.png" };
                steppeExhibit4.Description = "Крупная пастовая бусина из египетского стекла. Свидетельство широких торговых связей и высокого статуса.";
                steppeExhibit4.MuseumHallId = steppe.Id;
                _context.Exhibits.Update(steppeExhibit4);
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