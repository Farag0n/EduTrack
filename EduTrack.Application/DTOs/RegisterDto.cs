using System.ComponentModel.DataAnnotations;
using EduTrack.Domain.Enums;

namespace EduTrack.Application.DTOs;

public class RegisterDto
{
    [Required, EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public UserRole Role { get; set; }
}