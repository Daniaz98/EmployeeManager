using EmployeeManager.Infra.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly IGridFsService _service;

    public PhotoController(IGridFsService service)
    {
        _service = service;
    }

    [HttpPost("upload-photo")]
    public async Task<IActionResult> UploadPhoto([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0) throw new Exception("Arquivo inválido!");

        using var stream = file.OpenReadStream();
        var id = await _service.UploadFileAsync(stream, file.FileName, file.ContentType);
        return Ok(new { fotoId = id.ToString() });
    }

    // [HttpGet("photo/{id}")]
    // public async Task<IActionResult> GetPhoto(string id)
    // {
    //     if (!Object)
    // }
}