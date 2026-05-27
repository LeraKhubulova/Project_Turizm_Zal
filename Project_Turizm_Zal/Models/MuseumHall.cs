namespace Project_Turizm_Zal.Models
{
    public class MuseumHall
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Number {  get; set; }
        public string Description { get; set; }
        public List<Exhibition> Exhibitions { get; set; }
    }
}
