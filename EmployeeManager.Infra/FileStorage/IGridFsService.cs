using MongoDB.Bson;

namespace EmployeeManager.Infra.Services;

public interface IGridFsService
{
    Task<ObjectId> UploadFileAsync(Stream stream, string fileName,string contentType);
    Task<Stream> DownloadFileAsync(ObjectId id);
    Task DeleteFileAsync(ObjectId id);
}