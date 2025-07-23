using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EmployeeManager.Infra.DTO;

public class UploadPhotoDto
{
    [Required]
    public string EmployeeId { get; set; }
    
    [Required]
    public IFormFile? File { get; set; }
}