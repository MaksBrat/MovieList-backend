using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MovieList.Core.Interfaces;

namespace MovieList.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public FileService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string destinationFolder)
        {
            string path = Path.Combine(destinationFolder, file.FileName);
            using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return path;
        }

        public void DeleteFile(string filePath)
        {
            string fullPath = Path.Combine(_appEnvironment.WebRootPath, filePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
