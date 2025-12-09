using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Domain.Entities;

public class AcademicProgram
{
    [Key] public int Id { get; set; }
    [Column(TypeName = "varchar")] public string ProgramName { get; set; }
    [Column(TypeName = "varchar")] public string Faculty { get; set; }
    public int Credits { get; set; }
    
    //FK para relacion de muchos a muchos
    public List<Student> Students { get; set; } = new();
}