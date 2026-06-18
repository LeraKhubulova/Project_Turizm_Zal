using Microsoft.AspNetCore.Http;

namespace Project_Turizm_Zal.Services
{
    public interface IFileService
    {
        Task<string?> SaveImageAsync(IFormFile? file, string folderName, CancellationToken cancellationToken);
    }
}