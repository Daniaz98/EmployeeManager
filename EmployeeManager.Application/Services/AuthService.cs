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
    private readonly ITokenService _tokenService;
    private readonly IEmployeeRepository _employeeRepository;

    public AuthService(IUserRepository userRepository, IConfiguration configuration,  IEmployeeRepository employeeRepository,  ITokenService tokenService)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _employeeRepository = employeeRepository;
        _tokenService = tokenService;
    }
    
    public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
        if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            return null;
        }
        
        user.LastLogin = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        var token = _tokenService.GenerateToken(user);

        return new TokenResponseDto
        {
            Token = token,
            ExpiresAt = _tokenService.GetTokenExpiration(),
            Username = user.Username,
            Role = user.Role
        };
    }

    public async Task<TokenResponseDto?> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(registerDto.Username);
        if (existingUser != null)
            return null;
        
        var existingEmail = await _userRepository.GetByEmailAsync(registerDto.Email);
        if (existingEmail != null)
            return null;
        
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = HashPassword(registerDto.Password),
            Role = registerDto.Role,
            CreatedAt = DateTime.UtcNow,
        };
        
        await _userRepository.CreateAsync(user);

        var token = _tokenService.GenerateToken(user);

        return new TokenResponseDto
        {
            Token = token,
            ExpiresAt = _tokenService.GetTokenExpiration(),
            Username = user.Username,
            Role = user.Role
        };
    }

    public async Task<bool> ValidateUserAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return user != null;
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}