using EmployeeManager.Application.DTO;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Api.Controllers;

[ApiController]
[Route("employee")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var employees = await _service.GetEmployeesAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(string? id)
    {
        var employee = await _service.GetEmployeeByIdAsync(id);
        return Ok(employee);
    }

    [HttpPost]
    //[Authorize(Roles = "supervisor")]
    public async Task<ActionResult> Create([FromForm] CreateEmployeeDto dto)
    {
        await _service.CreateEmployeeAsync(dto);
        return Ok(dto);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "supervisor")]
    public async Task<IActionResult> Update(string id, [FromForm] UpdateEmployeeDto dto)
    {
        await _service.UpdateEmployeeAsync(id, dto);
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "supervisor")]
    public async Task<IActionResult> Delete(string? id)
    {
        await _service.DeleteEmployeeAsync(id);
        return Ok("Excluído com sucesso!");
    }
}