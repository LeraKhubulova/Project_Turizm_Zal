using Microsoft.AspNetCore.Http;

namespace Project_Turizm_Zal.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        private readonly string[] _allowedExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

        private const long MaxFileSize = 5 * 1024 * 1024;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string?> SaveImageAsync(IFormFile? file, string folderName, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            if (file.Length > MaxFileSize)
            {
                return null;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!_allowedExtensions.Contains(extension))
            {
                return null;
            }

            var safeFolderName = folderName
                .Replace("/", "")
                .Replace("\\", "");

            var uploadsFolder = Path.Combine(
                _environment.WebRootPath,
                "images",
                "uploads",
                safeFolderName
            );

            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(uploadsFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            return $"/images/uploads/{safeFolderName}/{fileName}";
        }
    }
}