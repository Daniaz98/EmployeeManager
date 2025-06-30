using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace EmployeeManager.Infra.Services;

public interface IGridFsService
{
    Task<string> UploadFileAsync(IFormFile file);
    Task<Stream> DownloadFileAsync(string id);
    Task DeleteFileAsync(ObjectId id);
}