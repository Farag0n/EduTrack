using EduTrack.Application.DTOs;

namespace EduTrack.Application.Interfaces;

public interface IStudentService
{
    Task<StudentDtos.StudentResponseDto?> GetStudentByIdAsync(int id);
    Task<StudentDtos.StudentResponseDto?> GetStudentByEmailAsync(string email);
    Task<IEnumerable<StudentDtos.StudentResponseDto>> GetAllStudentsAsync();
    Task<StudentDtos.StudentResponseDto> CreateStudentAsync(StudentDtos.StudentCreateDTO studentCreateDto);
    Task<StudentDtos.StudentResponseDto?> UpdateStudentAsync(StudentDtos.StudentUpdateDTO studentUpdateDto);
    Task<StudentDtos.StudentResponseDto?> DeleteStudentAsync(int id);
}