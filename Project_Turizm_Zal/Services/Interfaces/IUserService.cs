using Project_Turizm_Zal.Models;

namespace Project_Turizm_Zal.Services
{
    public interface IUserService
    {
        Task<bool> IsUserExists(string email, CancellationToken cancellationToken);
        Task<User> Login(string email, string password, CancellationToken cancellationToken);
        Task<bool> Register(User user, CancellationToken cancellationToken);
    }
}