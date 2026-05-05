namespace Project_Turizm_Zal.Models
{
    public class Exhibition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Exhibit> Exhibits { get; set; }

        public Exhibition(string name, string description, string image)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Image = image;
            Exhibits = new List<Exhibit>();
        }

        public void AddExhibit(Exhibit exhibit)
        {
            Exhibits.Add(exhibit);
        }

    }
}
