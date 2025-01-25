//IFileStorage: للتعامل مع تخزين الملفات

namespace Gameoria.Application.Common.Interfaces
{
    public interface IFileStorage
    {
        Task<string> SaveFileAsync(string containerName, string fileName, Stream fileStream);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
        Task<Stream> GetFileAsync(string containerName, string fileName);
        string GetFileUrl(string containerName, string fileName);
    }
}
