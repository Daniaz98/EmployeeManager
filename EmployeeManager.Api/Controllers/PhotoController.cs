using EmployeeManager.Application.Interfaces;
using EmployeeManager.Infra.DTO;
using EmployeeManager.Infra.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EmployeeManager.Api.Controllers;

[ApiController]
[Route("api/photo")]
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

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadPhoto(string id)
    {
        var stream = await _employeeService.DownloadPhotoAsync(id);
        
        if (stream == null) return NotFound("Arquivo não encontrado");
        
        return File(stream, "image/jpeg");
    }
}