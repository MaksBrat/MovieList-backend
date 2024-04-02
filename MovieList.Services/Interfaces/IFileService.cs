using Microsoft.AspNetCore.Http;

namespace MovieList.Core.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string destinationFolder);
        void DeleteFile(string filePath);
    }
}
