namespace Project_Turizm_Zal.Models
{
    public class Exhibit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Images { get; set; }
        public string Description { get; set; }


        //public Guid ExhibitionId { get; set; }
        //public Exhibition Exhibition { get; set; }

        public Guid MuseumHallId { get; set; }
        public MuseumHall MuseumHall { get; set; }



        //public Exhibit(string name, List<string> images, string description, Guid exhibitionId)
        //{
        //    Id = Guid.NewGuid();
        //    Name = name;
        //    Images = images;
        //    Description = description;
        //    ExhibitionId = exhibitionId;
        //}


        public Exhibit(string name, List<string> images, string description, Guid museumHallId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Images = images;
            Description = description;
            MuseumHallId = museumHallId;
        }
    }
}
