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
    }
}
