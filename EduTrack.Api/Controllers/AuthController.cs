using EduTrack.Application.DTOs;
using EduTrack.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    // POST: api/Auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            // El UserService maneja la autenticación y genera el token
            var token = await _userService.AuthenticateAsync(loginDto);
            
            return Ok(new 
            { 
                Token = token,
                Message = "Login exitoso" 
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    // POST: api/Auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            // El UserService maneja el registro y genera el token automáticamente
            var token = await _userService.RegisterAsync(registerDto);
            
            return Ok(new 
            { 
                Token = token,
                Message = "Registro exitoso" 
            });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    // POST: api/Auth/validate
    [HttpPost("validate")]
    public IActionResult ValidateToken([FromHeader] string authorization)
    {
        if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Bearer "))
            return Unauthorized(new { Message = "Token no proporcionado o formato incorrecto" });

        return Ok(new { Message = "Token válido" });
    }
}