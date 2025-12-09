using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduTrack.Domain.Enums;

namespace EduTrack.Domain.Entities;

public class Student
{
    [Key] public int Id { get; set; }
    [Column(TypeName = "varchar(100)")] public string Name { get; set; }
    [Column(TypeName = "varchar(100)")] public string LastName { get; set; }
    public int Age { get; set; }
    [Column(TypeName = "varchar(100)")] public string DocNumber { get; set; }
    [Column(TypeName = "varchar(100)")] public string Email { get; set; }
    [Column(TypeName = "varchar(100)")] public string PhoneNumber { get; set; }
    public StudentState State { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow; //Registra automaticamente la fecha de registro
    public int Semester { get; set; }
    
    //FK para relacion de muchos a muchos
    public List<AcademicProgram> AcademicPrograms { get; set; } = new();
}