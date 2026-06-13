using Microsoft.EntityFrameworkCore;
using Project_Turizm_Zal.Models;

namespace Project_Turizm_Zal.Data
{
    public class MuseumContext : DbContext
    {
        public MuseumContext(DbContextOptions<MuseumContext> options)
        : base(options)
        {
        }
        //public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Exhibit> Exhibits { get; set; }
        public DbSet<MuseumHall> Halls { get; set; }
        public DbSet<User> Users { get; set; }
    }
}