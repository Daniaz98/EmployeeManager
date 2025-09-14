using EmployeeManager.Application.DTO;
using EmployeeManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Api.Controllers;

[ApiController]
[Route("departments")]
public class DepartmentController : ControllerBase
{
    private readonly IEmployeeService _service;

    public DepartmentController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<DepartmentDto>> GetDepartmentsAsync()
    {
        var result = await _service.GetDepartmentsAsync();
        return result;
    }
}