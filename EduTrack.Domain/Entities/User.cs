using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduTrack.Domain.Enums;

namespace EduTrack.Domain.Entities;

public class User
{
    [Key] public int Id { get; set; }
    [Column(TypeName = "varchar")] public string Email { get; set; }
    [Column(TypeName = "varchar")] public string Username { get; set; }
    [Column(TypeName = "varchar")] public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
}