using Project_Turizm_Zal.Models;

namespace Project_Turizm_Zal.Services
{
    public interface IHallService
    {
        Task<List<MuseumHall>> GetAllHalls(CancellationToken cancellationToken);

        Task<MuseumHall?> GetHallById(Guid id, CancellationToken cancellationToken);

        Task<MuseumHall?> GetHallWithExhibits(Guid id, CancellationToken cancellationToken);

        Task<Exhibit?> GetExhibitById(Guid id, CancellationToken cancellationToken);

        Task<Exhibit?> GetExhibitWithHall(Guid id, CancellationToken cancellationToken);

        Task<bool> CreateHall(MuseumHall hall, CancellationToken cancellationToken);

        Task<bool> AddExhibit(Exhibit exhibit, CancellationToken cancellationToken);
        Task<List<MuseumHall>> GetAllHallsWithExhibits(CancellationToken cancellationToken);

        Task<bool> DeleteExhibit(Guid id, CancellationToken cancellationToken);
    }
}