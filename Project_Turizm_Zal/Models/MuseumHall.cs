namespace Project_Turizm_Zal.Models
{
    public class MuseumHall
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Number {  get; set; }
        public string Description { get; set; }

        public string ImagePath { get; set; }
        public string Slogan { get; set; }
        public string LeftColumnText { get; set; }
        public string MapImagePath { get; set; }






        public List<Exhibition> Exhibitions { get; set; }
    }
}
