using EduTrack.Application.DTOs;
using EduTrack.Application.Interfaces;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Interfaces;

namespace EduTrack.Application.Services;

public class AcademicProgramService : IAcademicProgramService
{
    private readonly IAcademicProgramRepository _academicProgramRepository;

    public AcademicProgramService(IAcademicProgramRepository academicProgramRepository)
    {
        _academicProgramRepository = academicProgramRepository;
    }

    public async Task<AcademicProgramDtos.AcademicProgramResponseDto?> GetAcademicProgramByIdAsync(int id)
    {
        var program = await _academicProgramRepository.GetAcademicProgramById(id);
        return program == null ? null : MapToResponseDto(program);
    }

    public async Task<AcademicProgramDtos.AcademicProgramResponseDto?> GetAcademicProgramByNameAsync(string name)
    {
        var program = await _academicProgramRepository.GetAcademicProgramByName(name);
        return program == null ? null : MapToResponseDto(program);
    }

    public async Task<IEnumerable<AcademicProgramDtos.AcademicProgramResponseDto>> GetAllAcademicProgramsAsync()
    {
        var programs = await _academicProgramRepository.GetAllAcademicPrograms();
        return programs.Select(MapToResponseDto);
    }

    public async Task<AcademicProgramDtos.AcademicProgramResponseDto> CreateAcademicProgramAsync(AcademicProgramDtos.AcademicProgramCreateDto academicProgramCreateDto)
    {
        // Verificar si el nombre del programa ya existe
        var existingProgram = await _academicProgramRepository.GetAcademicProgramByName(academicProgramCreateDto.ProgramName);
        if (existingProgram != null)
            throw new InvalidOperationException("Ya existe un programa académico con ese nombre");

        var program = new AcademicProgram
        {
            ProgramName = academicProgramCreateDto.ProgramName,
            Faculty = academicProgramCreateDto.Faculty,
            Credits = academicProgramCreateDto.Credits
        };

        var createdProgram = await _academicProgramRepository.CreateAcademicProgram(program);
        return MapToResponseDto(createdProgram);
    }

    public async Task<AcademicProgramDtos.AcademicProgramResponseDto?> UpdateAcademicProgramAsync(AcademicProgramDtos.AcademicProgramUpdateDto academicProgramUpdateDto)
    {
        var existingProgram = await _academicProgramRepository.GetAcademicProgramById(academicProgramUpdateDto.Id);
        if (existingProgram == null)
            return null;

        existingProgram.ProgramName = academicProgramUpdateDto.ProgramName;
        existingProgram.Faculty = academicProgramUpdateDto.Faculty;
        existingProgram.Credits = academicProgramUpdateDto.Credits;

        var updatedProgram = await _academicProgramRepository.UpdateAcademicProgram(existingProgram);
        return updatedProgram == null ? null : MapToResponseDto(updatedProgram);
    }

    public async Task<AcademicProgramDtos.AcademicProgramResponseDto?> DeleteAcademicProgramAsync(int id)
    {
        var deletedProgram = await _academicProgramRepository.DeleteAcademicProgram(id);
        return deletedProgram == null ? null : MapToResponseDto(deletedProgram);
    }

    // Mapeo de Entity a DTO
    private AcademicProgramDtos.AcademicProgramResponseDto MapToResponseDto(AcademicProgram academicProgram)
    {
        return new AcademicProgramDtos.AcademicProgramResponseDto
        {
            Id = academicProgram.Id,
            ProgramName = academicProgram.ProgramName,
            Faculty = academicProgram.Faculty,
            Credits = academicProgram.Credits
        };
    }
}