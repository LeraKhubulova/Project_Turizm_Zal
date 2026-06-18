using Microsoft.AspNetCore.Http;

namespace Project_Turizm_Zal.Models.ViewModels
{
    public class CreateHallViewModel
    {
        public string Name { get; set; } = string.Empty;

        public int Number { get; set; }

        public string Description { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }

        public string Slogan { get; set; } = string.Empty;

        public string LeftColumnText { get; set; } = string.Empty;

        public string MapData { get; set; } = string.Empty;
    }
}