using System.Text.Json.Serialization;

namespace Project_Turizm_Zal.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        public User(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            RegistrationDate = DateTime.Now;
        }

        [JsonConstructor]
        public User(Guid id, string name, string email, string password, DateTime registrationDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            RegistrationDate = registrationDate;
        }

    }
}
