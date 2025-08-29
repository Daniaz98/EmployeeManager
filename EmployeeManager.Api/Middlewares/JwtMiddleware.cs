using EmployeeManager.Application.Interfaces;
using EmployeeManager.Infra.Interfaces;

namespace EmployeeManager.Api.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenService _tokenService;

    public JwtMiddleware(RequestDelegate next, ITokenService tokenService)
    {
        _next = next;
        _tokenService = tokenService;
    }

    public async Task Invoke(HttpContext context, IUserRepository userRepository)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            var userId = _tokenService.ValidateToken(token);
            if (userId != null)
            {
                var user = await userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    context.Items["User"] = user;
                    context.Items["UserId"] = userId;
                }
            }
        }
        
        await _next(context);
    }
}