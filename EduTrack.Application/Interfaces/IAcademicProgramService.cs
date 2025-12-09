using EduTrack.Application.DTOs;

namespace EduTrack.Application.Interfaces;

public interface IAcademicProgramService
{
    Task<AcademicProgramDtos.AcademicProgramResponseDto?> GetAcademicProgramByIdAsync(int id);
    Task<AcademicProgramDtos.AcademicProgramResponseDto?> GetAcademicProgramByNameAsync(string name);
    Task<IEnumerable<AcademicProgramDtos.AcademicProgramResponseDto>> GetAllAcademicProgramsAsync();
    Task<AcademicProgramDtos.AcademicProgramResponseDto> CreateAcademicProgramAsync(AcademicProgramDtos.AcademicProgramCreateDto academicProgramCreateDto);
    Task<AcademicProgramDtos.AcademicProgramResponseDto?> UpdateAcademicProgramAsync(AcademicProgramDtos.AcademicProgramUpdateDto academicProgramUpdateDto);
    Task<AcademicProgramDtos.AcademicProgramResponseDto?> DeleteAcademicProgramAsync(int id);
}