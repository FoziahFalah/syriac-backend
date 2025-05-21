using Microsoft.AspNetCore.Http;
using SyriacSources.Backend.Domain.Common.Interfaces;
using SyriacSources.Backend.Domain.Common.Constants;
namespace SyriacSources.Backend.Infrastructure.Common.Services
{
    public class FileServices : IFileServices
    {
        private readonly string _fileRoot;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FileServices(string fileRoot, IHttpContextAccessor httpContextAccessor)
        {
            _fileRoot = Path.Combine(fileRoot, FilesConstants.UploadsFolder);
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> SaveFileAsync(string fileName, Stream fileStream)
        {
            string extension = Path.GetExtension(fileName);
            string newFileName = $"{Guid.NewGuid()}{extension}";
            string path = Path.Combine(_fileRoot, newFileName);
            if (!Directory.Exists(_fileRoot))
                Directory.CreateDirectory(_fileRoot);
            using var file = new FileStream(path, FileMode.CreateNew);
            await fileStream.CopyToAsync(file);
            // return relative path to use in frontend
            return Path.Combine(FilesConstants.UploadsFolder, newFileName).Replace("\\", "/");
        }
        public async Task<bool> FileExists(string filePath)
        {
            var fullPath = Path.Combine(_fileRoot, filePath);
            return await Task.FromResult(File.Exists(fullPath));
        }
        public Task DeleteFile(string filePath)
        {
            var fullPath = Path.Combine(_fileRoot, filePath);
            if (File.Exists(fullPath))
                File.Delete(fullPath);
            return Task.CompletedTask;
        }
        public async Task<Stream> GetFile(string filePath)
        {
            var fullPath = Path.Combine(_fileRoot, filePath);
            if (!File.Exists(fullPath)) return null!;
            return await Task.FromResult(File.OpenRead(fullPath));
        }
        public async Task<string> GetFileUrl(string filePath)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null) return string.Empty;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            return await Task.FromResult($"{baseUrl}/{filePath.Replace("\\", "/")}");
        }
        public Task EnsurePathExists(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir!);
            return Task.CompletedTask;
        }
    }
}

