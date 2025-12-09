using EduTrack.Application.DTOs;
using EduTrack.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requiere autenticación para todos los endpoints
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/Students
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    // GET: api/Students/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        
        if (student == null)
            return NotFound(new { Message = "Estudiante no encontrado" });

        return Ok(student);
    }

    // GET: api/Students/email/estudiante@example.com
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var student = await _studentService.GetStudentByEmailAsync(email);
        
        if (student == null)
            return NotFound(new { Message = "Estudiante no encontrado" });

        return Ok(student);
    }

    // POST: api/Students
    [HttpPost]
    [Authorize(Roles = "Admin")] // Solo Admin puede crear estudiantes
    public async Task<IActionResult> Create([FromBody] StudentDtos.StudentCreateDTO studentCreateDto)
    {
        try
        {
            var createdStudent = await _studentService.CreateStudentAsync(studentCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
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

    // PUT: api/Students/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] // Solo Admin puede actualizar estudiantes
    public async Task<IActionResult> Update(int id, [FromBody] StudentDtos.StudentUpdateDTO studentUpdateDto)
    {
        if (id != studentUpdateDto.Id)
            return BadRequest(new { Message = "ID no coincide" });

        try
        {
            var updatedStudent = await _studentService.UpdateStudentAsync(studentUpdateDto);
            
            if (updatedStudent == null)
                return NotFound(new { Message = "Estudiante no encontrado" });

            return Ok(updatedStudent);
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

    // DELETE: api/Students/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // Solo Admin puede eliminar estudiantes
    public async Task<IActionResult> Delete(int id)
    {
        var deletedStudent = await _studentService.DeleteStudentAsync(id);
        
        if (deletedStudent == null)
            return NotFound(new { Message = "Estudiante no encontrado" });

        return Ok(new { Message = "Estudiante eliminado exitosamente", Student = deletedStudent });
    }

    // GET: api/Students/search?name=juan
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? state)
    {
        var allStudents = await _studentService.GetAllStudentsAsync();
        
        var filteredStudents = allStudents.AsEnumerable();
        
        if (!string.IsNullOrEmpty(name))
        {
            filteredStudents = filteredStudents.Where(s => 
                s.Name.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                s.LastName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(state) && Enum.TryParse<Domain.Enums.StudentState>(state, true, out var stateEnum))
        {
            filteredStudents = filteredStudents.Where(s => s.State == stateEnum);
        }

        return Ok(filteredStudents);
    }
}