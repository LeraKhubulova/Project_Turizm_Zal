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

        public async Task<List<MuseumHall>> GetAllHalls(CancellationToken cancellationToken)
        {
            return await _context.Halls
                .AsNoTracking()
                .OrderBy(h => h.Number)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<MuseumHall>> GetAllHallsWithExhibits(CancellationToken cancellationToken)
        {
            return await _context.Halls
                .AsNoTracking()
                .Include(h => h.Exhibits)
                .OrderBy(h => h.Number)
                .ToListAsync(cancellationToken);
        }

        public async Task<MuseumHall?> GetHallById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Halls
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        }

        public async Task<MuseumHall?> GetHallWithExhibits(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Halls
                .AsNoTracking()
                .Include(h => h.Exhibits)
                .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        }

        public async Task<Exhibit?> GetExhibitById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Exhibits
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<Exhibit?> GetExhibitWithHall(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Exhibits
                .AsNoTracking()
                .Include(e => e.MuseumHall)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<bool> CreateHall(MuseumHall hall, CancellationToken cancellationToken)
        {
            var exists = await _context.Halls
                .AnyAsync(h => h.Name == hall.Name || h.Number == hall.Number, cancellationToken);

            if (exists)
            {
                return false;
            }

            if (hall.Id == Guid.Empty)
            {
                hall.Id = Guid.NewGuid();
            }

            _context.Halls.Add(hall);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> AddExhibit(Exhibit exhibit, CancellationToken cancellationToken)
        {
            var hallExists = await _context.Halls
                .AnyAsync(h => h.Id == exhibit.MuseumHallId, cancellationToken);

            if (!hallExists)
            {
                return false;
            }

            var exhibitExists = await _context.Exhibits
                .AnyAsync(e => e.Name == exhibit.Name && e.MuseumHallId == exhibit.MuseumHallId, cancellationToken);

            if (exhibitExists)
            {
                return false;
            }

            if (exhibit.Id == Guid.Empty)
            {
                exhibit.Id = Guid.NewGuid();
            }

            _context.Exhibits.Add(exhibit);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task<bool> UpdateExhibit(Exhibit exhibit, CancellationToken cancellationToken)
        {
            var existingExhibit = await _context.Exhibits
                .FirstOrDefaultAsync(e => e.Id == exhibit.Id, cancellationToken);

            if (existingExhibit == null)
            {
                return false;
            }

            var hallExists = await _context.Halls
                .AnyAsync(h => h.Id == exhibit.MuseumHallId, cancellationToken);

            if (!hallExists)
            {
                return false;
            }

            var duplicateExists = await _context.Exhibits
                .AnyAsync(e =>
                    e.Id != exhibit.Id &&
                    e.Name == exhibit.Name &&
                    e.MuseumHallId == exhibit.MuseumHallId,
                    cancellationToken);

            if (duplicateExists)
            {
                return false;
            }

            existingExhibit.Name = exhibit.Name;
            existingExhibit.Images = exhibit.Images;
            existingExhibit.Description = exhibit.Description;
            existingExhibit.ShortDescription = exhibit.ShortDescription;
            existingExhibit.MuseumHallId = exhibit.MuseumHallId;
            existingExhibit.CultureEra = exhibit.CultureEra;
            existingExhibit.FindLocation = exhibit.FindLocation;
            existingExhibit.Materials = exhibit.Materials;
            existingExhibit.Technique = exhibit.Technique;
            existingExhibit.Dimensions = exhibit.Dimensions;
            existingExhibit.Weight = exhibit.Weight;
            existingExhibit.Quantity = exhibit.Quantity;
            existingExhibit.Storage = exhibit.Storage;
            existingExhibit.Model3DUrl = exhibit.Model3DUrl;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteExhibit(Guid id, CancellationToken cancellationToken)
        {
            var exhibit = await _context.Exhibits
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (exhibit == null)
            {
                return false;
            }

            _context.Exhibits.Remove(exhibit);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}