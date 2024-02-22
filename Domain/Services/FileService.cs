using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public class FileService : IFileService
    {
        readonly string _folderPath;
        public FileService(string folderPath)
        {
            _folderPath = folderPath;   
        }

        public byte[] Download(string fileName)
        {
            string filePath = Path.Combine(_folderPath, fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            return File.ReadAllBytes(filePath);
        }

        public async Task Upload(IFormFile file)
        {
            string filePath = Path.Combine(_folderPath, file.FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}
