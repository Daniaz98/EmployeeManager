using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManager.Application.DTO;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;

namespace EmployeeManager.Application.Services;

public class AuthService :  IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IEmployeeRepository _employeeRepository;

    public AuthService(IUserRepository userRepository, IConfiguration configuration,  IEmployeeRepository employeeRepository)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _employeeRepository = employeeRepository;
    }
    
    public async Task<AuthResultDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            return null;
        
        var token = GenerateJwtToken(user);

        return new AuthResultDto
        {
            Token = token,
            Email = user.Email,
            Role = user.Role.ToString(),
        };
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _employeeRepository.GetEmployeeById(registerDto.EmployeeId);
        if (existingUser == null) 
            throw new Exception("User not found");
        
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        
        var user = new User
        {
            Email = registerDto.Email,
            Password = hashedPassword,
            Role = registerDto.Role,
            //EmployeeId = registerDto.EmployeeId
        };

        await _userRepository.AddUserAsync(user);
        Console.WriteLine($"User {user.Email} created");
        return true;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                issuer:  _configuration["Jwt:Issuer"],
                audience:  _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds
            );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}