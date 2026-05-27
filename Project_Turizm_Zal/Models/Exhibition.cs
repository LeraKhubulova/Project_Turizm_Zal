

namespace Project_Turizm_Zal.Models
{
    public class Exhibition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        private readonly List<Exhibit> _exhibits;
        public Guid HallId { get; set; }
        public MuseumHall Hall { get; set; }

        public Exhibition(string name, string description, string image, Guid hallId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Image = image;
            _exhibits = new List<Exhibit>();
            HallId = hallId;
        }

        public void AddExhibit(Exhibit exhibit)
        {
            if (exhibit == null) throw new ArgumentNullException("Exhibit is null");
            _exhibits.Add(exhibit);
        }

        public void RemoveExhibit(Guid exhibitId)
        {
            var exhibit = _exhibits.FirstOrDefault(x => x.Id == exhibitId);
            if (exhibit == null) throw new ArgumentException("Exhibit not found");
            _exhibits.Remove(exhibit);
        }

        public List<Exhibit> GetExhibits() => _exhibits;

        

        

    }
}
