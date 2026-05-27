namespace Project_Turizm_Zal.Models
{
    public class ExhibitViewModel
    {
            public Guid Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public List<string> Images { get; set; } = new();

            public string Description { get; set; } = string.Empty;

            public string ExhibitionName { get; set; } = string.Empty;
    }
}
