using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace EmployeeManager.Infra.Services;

public class GridFsService : IGridFsService
{
    private readonly IGridFSBucket _bucket;

    public GridFsService(IMongoDatabase database)
    {
        _bucket = new GridFSBucket(database);
    }
    
    public async Task<string> UploadFileAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var options = new GridFSUploadOptions
        {
            Metadata = new BsonDocument{ { "ContentType", file.ContentType } }
        };
        
        var fileId = await _bucket.UploadFromStreamAsync(file.FileName, stream, options);
        return fileId.ToString();
    }

    public async Task<Stream> DownloadFileAsync(string photoId)
    {
       var objectId = ObjectId.Parse(photoId);
       return await _bucket.OpenDownloadStreamAsync(objectId);
    }

    public async Task DeleteFileAsync(ObjectId id)
    {
        await _bucket.DeleteAsync(id);
    }
}