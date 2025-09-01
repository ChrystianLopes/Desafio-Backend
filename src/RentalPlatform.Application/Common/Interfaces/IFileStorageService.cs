using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Common.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken);
}