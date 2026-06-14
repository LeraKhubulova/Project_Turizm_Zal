using Project_Turizm_Zal.Models;

namespace Project_Turizm_Zal.Services
{
    public interface IHallService
    {
        Task<Exhibit> GetExhibitById(Guid id, CancellationToken cancellationToken);
        //Task<Exhibition> GetExhibitionById(Guid id, CancellationToken cancellationToken);
        Task<MuseumHall> GetHallById(Guid id, CancellationToken cancellationToken);
    }
}