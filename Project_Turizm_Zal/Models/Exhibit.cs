namespace Project_Turizm_Zal.Models
{
    public class Exhibit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Images { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        public Guid MuseumHallId { get; set; }
        public MuseumHall MuseumHall { get; set; }

        public string? CultureEra { get; set; }      
        public string? FindLocation { get; set; }    
        public string? Materials { get; set; }       
        public string? Technique { get; set; }     
        public string? Dimensions { get; set; }     
        public string? Weight { get; set; }         
        public string? Quantity { get; set; }        
        public string? Storage { get; set; }         
        public string? Model3DUrl { get; set; }

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
