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
    
    public async Task<ObjectId> UploadFileAsync(Stream stream, string fileName, string contentType)
    {
        var options = new GridFSUploadOptions
        {
            Metadata = new BsonDocument { { "contentType", contentType } },
        };
        
        return await _bucket.UploadFromStreamAsync(fileName, stream, options);
    }

    public async Task<Stream> DownloadFileAsync(ObjectId id)
    {
        var stream = new MemoryStream();
        await _bucket.DownloadToStreamAsync(id, stream);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    public async Task DeleteFileAsync(ObjectId id)
    {
        await _bucket.DeleteAsync(id);
    }
}