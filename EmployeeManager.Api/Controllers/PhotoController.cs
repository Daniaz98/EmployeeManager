using EmployeeManager.Application.Interfaces;
using EmployeeManager.Infra.DTO;
using EmployeeManager.Infra.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EmployeeManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly IGridFsService _service;
    private readonly IEmployeeService _employeeService;

    public PhotoController(IGridFsService service, IEmployeeService employeeService)
    {
        _service = service;
        _employeeService = employeeService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadPhoto([FromForm] UploadPhotoDto dto)
    {
        await _employeeService.UploadEmployeePhotoAsync(dto.EmployeeId, dto.File);
        return Ok("Foto salva e vinculada ao funcionário.");
    }

    // [HttpGet("photo/{id}")]
    // public async Task<IActionResult> DownloadPhoto(ObjectId id)
    // {
    //     //if (ObjectId == null) return NotFound();
    //     
    //     await using var stream = new MemoryStream();
    //     var file = await _service.DownloadFileAsync(id);
    //     stream.Position = 0;
    //     return File(stream, "image/jpeg");
    // }
}