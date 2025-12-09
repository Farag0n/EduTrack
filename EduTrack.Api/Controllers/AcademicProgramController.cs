using EduTrack.Application.DTOs;
using EduTrack.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requiere autenticación para todos los endpoints
public class AcademicProgramController : ControllerBase
{
    private readonly IAcademicProgramService _academicProgramService;

    public AcademicProgramController(IAcademicProgramService academicProgramService)
    {
        _academicProgramService = academicProgramService;
    }

    // GET: api/AcademicPrograms
    [HttpGet]
    [AllowAnonymous] // Permitir acceso sin autenticación para ver programas
    public async Task<IActionResult> GetAll()
    {
        var programs = await _academicProgramService.GetAllAcademicProgramsAsync();
        return Ok(programs);
    }

    // GET: api/AcademicPrograms/5
    [HttpGet("{id}")]
    [AllowAnonymous] // Permitir acceso sin autenticación
    public async Task<IActionResult> GetById(int id)
    {
        var program = await _academicProgramService.GetAcademicProgramByIdAsync(id);
        
        if (program == null)
            return NotFound(new { Message = "Programa académico no encontrado" });

        return Ok(program);
    }

    // GET: api/AcademicPrograms/name/Ingeniería
    [HttpGet("name/{name}")]
    [AllowAnonymous] // Permitir acceso sin autenticación
    public async Task<IActionResult> GetByName(string name)
    {
        var program = await _academicProgramService.GetAcademicProgramByNameAsync(name);
        
        if (program == null)
            return NotFound(new { Message = "Programa académico no encontrado" });

        return Ok(program);
    }

    // POST: api/AcademicPrograms
    [HttpPost]
    [Authorize(Roles = "Admin")] // Solo Admin puede crear programas
    public async Task<IActionResult> Create([FromBody] AcademicProgramDtos.AcademicProgramCreateDto programCreateDto)
    {
        try
        {
            var createdProgram = await _academicProgramService.CreateAcademicProgramAsync(programCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProgram.Id }, createdProgram);
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

    // PUT: api/AcademicPrograms/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] // Solo Admin puede actualizar programas
    public async Task<IActionResult> Update(int id, [FromBody] AcademicProgramDtos.AcademicProgramUpdateDto programUpdateDto)
    {
        if (id != programUpdateDto.Id)
            return BadRequest(new { Message = "ID no coincide" });

        try
        {
            var updatedProgram = await _academicProgramService.UpdateAcademicProgramAsync(programUpdateDto);
            
            if (updatedProgram == null)
                return NotFound(new { Message = "Programa académico no encontrado" });

            return Ok(updatedProgram);
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

    // DELETE: api/AcademicPrograms/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // Solo Admin puede eliminar programas
    public async Task<IActionResult> Delete(int id)
    {
        var deletedProgram = await _academicProgramService.DeleteAcademicProgramAsync(id);
        
        if (deletedProgram == null)
            return NotFound(new { Message = "Programa académico no encontrado" });

        return Ok(new { Message = "Programa académico eliminado exitosamente", Program = deletedProgram });
    }

    // GET: api/AcademicPrograms/search?faculty=Ingeniería
    [HttpGet("search")]
    [AllowAnonymous] // Permitir acceso sin autenticación
    public async Task<IActionResult> Search([FromQuery] string? faculty, [FromQuery] int? minCredits)
    {
        var allPrograms = await _academicProgramService.GetAllAcademicProgramsAsync();
        
        var filteredPrograms = allPrograms.AsEnumerable();
        
        if (!string.IsNullOrEmpty(faculty))
        {
            filteredPrograms = filteredPrograms.Where(p => 
                p.Faculty.Contains(faculty, StringComparison.OrdinalIgnoreCase));
        }

        if (minCredits.HasValue)
        {
            filteredPrograms = filteredPrograms.Where(p => p.Credits >= minCredits.Value);
        }

        return Ok(filteredPrograms);
    }
}