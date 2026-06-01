using Microsoft.EntityFrameworkCore;
using Project_Turizm_Zal.Data;
using Project_Turizm_Zal.Models;

namespace Project_Turizm_Zal.Services
{
    public class HallService : IHallService
    {
        private readonly MuseumContext _context;

        public HallService(MuseumContext context)
        {
            _context = context;
        }

        public async Task<MuseumHall> GetHallById(Guid id, CancellationToken cancellationToken)
        {
            var task = await _context.Halls
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
            if (task == null)
            {
                throw new NullReferenceException("Wrong hall id");
            }
            return task;
        }

        public async Task<Exhibition> GetExhibitionById(Guid id, CancellationToken cancellationToken)
        {
            var task = await _context.Exhibitions
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
            if (task == null)
            {
                throw new NullReferenceException("Wrong exhibition id");
            }
            return task;
        }
        public async Task<Exhibit> GetExhibitById(Guid id, CancellationToken cancellationToken)
        {
            var task = await _context.Exhibits
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
            if (task == null)
            {
                throw new NullReferenceException("Wrong exhibit id");
            }
            return task;
        }

        // В HallService добавьте следующие методы:

        // ========== МЕТОДЫ ДЛЯ РАБОТЫ С ЗАЛАМИ (MuseumHall) ==========

        public async Task<bool> AddHall(MuseumHall hall, CancellationToken cancellationToken)
        {
            // Проверяем, существует ли уже зал с таким Id
            if (await _context.Halls.FirstOrDefaultAsync(h => h.Id == hall.Id, cancellationToken) != null)
                return false;

            // Проверяем, существует ли зал с таким же номером (опционально)
            if (await _context.Halls.FirstOrDefaultAsync(h => h.Number == hall.Number, cancellationToken) != null)
                return false;

            _context.Halls.Add(hall);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteHall(Guid id, CancellationToken cancellationToken)
        {
            var hall = await _context.Halls
                .Include(h => h.Exhibitions)
                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

            if (hall == null)
                return false;

            // Если нужно удалить также все выставки и экспонаты в зале
            // (в зависимости от бизнес-логики)
            if (hall.Exhibitions != null && hall.Exhibitions.Any())
            {
                var exhibitionIds = hall.Exhibitions.Select(e => e.Id).ToList();

                // Удаляем все экспонаты, связанные с этими выставками
                var exhibitsToDelete = await _context.Exhibits
                    .Where(e => exhibitionIds.Contains(e.ExhibitionId))
                    .ToListAsync(cancellationToken);

                _context.Exhibits.RemoveRange(exhibitsToDelete);

                // Удаляем выставки
                _context.Exhibitions.RemoveRange(hall.Exhibitions);
            }

            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        // ========== МЕТОДЫ ДЛЯ РАБОТЫ С ЭКСПОНАТАМИ (Exhibit) ==========

        public async Task<bool> DeleteExhibit(Guid id, CancellationToken cancellationToken)
        {
            var exhibit = await _context.Exhibits
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (exhibit == null)
                return false;

            _context.Exhibits.Remove(exhibit);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        // Обновленный метод AddExhibit с проверкой существования выставки
        public async Task<bool> AddExhibit(Exhibit exhibit, CancellationToken cancellationToken)
        {
            // Проверяем, существует ли экспонат с таким Id
            if (await _context.Exhibits.FirstOrDefaultAsync(u => u.Id == exhibit.Id, cancellationToken) != null)
                return false;

            // Проверяем, существует ли выставка, к которой привязываем экспонат
            var exhibition = await _context.Exhibitions
                .FirstOrDefaultAsync(e => e.Id == exhibit.ExhibitionId, cancellationToken);

            if (exhibition == null)
                return false;

            _context.Exhibits.Add(exhibit);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        // Дополнительный метод: обновление экспоната
        public async Task<bool> UpdateExhibit(Exhibit exhibit, CancellationToken cancellationToken)
        {
            var existingExhibit = await _context.Exhibits
                .FirstOrDefaultAsync(e => e.Id == exhibit.Id, cancellationToken);

            if (existingExhibit == null)
                return false;

            existingExhibit.Name = exhibit.Name;
            existingExhibit.Images = exhibit.Images;
            existingExhibit.Description = exhibit.Description;
            existingExhibit.ExhibitionId = exhibit.ExhibitionId;

            _context.Exhibits.Update(existingExhibit);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
