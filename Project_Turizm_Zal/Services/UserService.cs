using System.Text.Json;
using Project_Turizm_Zal.Models;

namespace Project_Turizm_Zal.Services
{
    public class UserService
    {
        private readonly string _usersFilePath;
        private List<User> _users = new List<User>();  

        public UserService(IWebHostEnvironment env)
        {
            _usersFilePath = Path.Combine(env.ContentRootPath, "Data", "users.json");
            LoadUsers();
        }

        private void LoadUsers()
        {
            if (!File.Exists(_usersFilePath))
            {
                _users = new List<User>();
                SaveUsers();
                return;
            }

            var json = File.ReadAllText(_usersFilePath);
            _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        private void SaveUsers()
        {
            var json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            var directory = Path.GetDirectoryName(_usersFilePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            File.WriteAllText(_usersFilePath, json);
        }

        public bool Register(User user)
        {
            if (_users.Any(u => u.Email == user.Email))
                return false;

            _users.Add(user);
            SaveUsers();
            return true;
        }

        public User Login(string email, string password)
        {
            return _users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool IsUserExists(string email)
        {
            return _users.Any(u => u.Email == email);
        }
    }
}
