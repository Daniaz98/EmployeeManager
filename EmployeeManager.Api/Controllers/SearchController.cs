using EmployeeManager.Application.DTO;
using EmployeeManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Api.Controllers;

[ApiController]
[Route("search")]
[Produces("application/json")]
public class SearchController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public SearchController(IEmployeeService employeeService)
    {
        _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
    }

    /// <summary>
    /// Realiza busca geral de funcionários com filtros e paginação
    /// </summary>
    /// <param name="searchDto">Critérios de busca</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado paginado da busca</returns>
    [HttpGet]
    [ProducesResponseType(typeof(EmployeeSearchResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EmployeeSearchResultDto>> SearchEmployees(
        [FromQuery] SearchEmployeesDto searchDto,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.SearchEmployeesAsync(searchDto, cancellationToken);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Parâmetros inválidos",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Title = "Erro interno do servidor",
                Detail = "Ocorreu um erro inesperado durante a busca",
                Status = StatusCodes.Status500InternalServerError
            });
        }   
    }

}