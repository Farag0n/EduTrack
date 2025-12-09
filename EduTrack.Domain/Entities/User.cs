using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduTrack.Domain.Enums;

namespace EduTrack.Domain.Entities;

public class User
{
    [Key] public int Id { get; set; }
    [Column(TypeName = "varchar(100)")] public string Email { get; set; }
    [Column(TypeName = "varchar(100)")] public string Username { get; set; }
    [Column(TypeName = "varchar(100)")] public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    
    //Refresh token
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }
}