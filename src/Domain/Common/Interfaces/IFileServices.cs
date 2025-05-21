using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace SyriacSources.Backend.Domain.Common.Interfaces
{
    public interface IFileServices
    {
        Task<string> SaveFileAsync(string fileName, Stream fileStream);
        Task<bool> FileExists(string filePath);
        Task DeleteFile(string filePath);
        Task<Stream> GetFile(string filePath);
        Task<string> GetFileUrl(string filePath);
        Task EnsurePathExists(string filePath);
    }
}





