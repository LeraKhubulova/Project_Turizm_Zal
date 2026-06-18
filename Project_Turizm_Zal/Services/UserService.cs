using Microsoft.EntityFrameworkCore;
using Project_Turizm_Zal.Data;
using Project_Turizm_Zal.Models;
using static Project_Turizm_Zal.Models.User;

namespace Project_Turizm_Zal.Services
{
    public class UserService : IUserService
    {
        private readonly MuseumContext _context;

        public UserService(MuseumContext context)
        {
            _context = context;
        }

        public async Task<bool> Register(User user, CancellationToken cancellationToken)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Email == user.Email, cancellationToken);

            if (exists)
            {
                return false;
            }

            if (user.Email == "admin@admin.com" && user.Password == "admin")
            {
                user.Role = UserRole.Admin;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<User?> Login(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password, cancellationToken);

            return user;
        }

        public async Task<bool> IsUserExists(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
}
