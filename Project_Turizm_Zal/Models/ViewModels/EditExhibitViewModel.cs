using Microsoft.AspNetCore.Http;
using Project_Turizm_Zal.Models;

namespace Project_Turizm_Zal.Models.ViewModels
{
    public class EditExhibitViewModel
    {
        public Guid Id { get; set; }

        public Guid MuseumHallId { get; set; }

        public List<MuseumHall> Halls { get; set; } = new List<MuseumHall>();

        public string Name { get; set; } = string.Empty;

        public string ExistingImagePath { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string? CultureEra { get; set; }

        public string? FindLocation { get; set; }

        public string? Materials { get; set; }

        public string? Technique { get; set; }

        public string? Dimensions { get; set; }

        public string? Weight { get; set; }

        public string? Quantity { get; set; }

        public string? Storage { get; set; }

        public string? Model3DUrl { get; set; }
    }
}