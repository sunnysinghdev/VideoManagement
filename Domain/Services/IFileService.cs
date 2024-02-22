using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public interface IFileService
    {
        byte[] Download(string fileName);
        Task Upload(IFormFile file);
    }
}
