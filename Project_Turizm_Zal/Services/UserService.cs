using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Project_Turizm_Zal.Data;
using Project_Turizm_Zal.Models;

namespace Project_Turizm_Zal.Services
{
    public class UserService
    {
        private readonly MuseumContext _context;

        public UserService(MuseumContext context)
        {
            _context = context;
        }

        public async Task<bool> Register(User user, CancellationToken cancellationToken)
        {
            if ( await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken) != null) return false;
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<User> Login(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password, cancellationToken);
            if (user == null) throw new Exception("Wrong email or password");
            return user;
        }

        public async Task<bool> IsUserExists(string email, CancellationToken cancellationToken)
        {
            var result = await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
            return result;
        }
    }
}
