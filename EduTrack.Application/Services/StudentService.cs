using EduTrack.Application.DTOs;
using EduTrack.Application.Interfaces;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Interfaces;

namespace EduTrack.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IAcademicProgramRepository _academicProgramRepository;

    public StudentService(IStudentRepository studentRepository, IAcademicProgramRepository academicProgramRepository)
    {
        _studentRepository = studentRepository;
        _academicProgramRepository = academicProgramRepository;
    }

    public async Task<StudentDtos.StudentResponseDto?> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentById(id);
        return student == null ? null : MapToStudentResponseDto(student);
    }

    public async Task<StudentDtos.StudentResponseDto?> GetStudentByEmailAsync(string email)
    {
        var student = await _studentRepository.GetStudentByEmail(email);
        return student == null ? null : MapToStudentResponseDto(student);
    }

    public async Task<IEnumerable<StudentDtos.StudentResponseDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllStudents();
        return students.Select(MapToStudentResponseDto);
    }

    public async Task<StudentDtos.StudentResponseDto> CreateStudentAsync(StudentDtos.StudentCreateDTO studentCreateDto)
    {
        // Verificar si el email ya existe
        var existingStudent = await _studentRepository.GetStudentByEmail(studentCreateDto.Email);
        if (existingStudent != null)
            throw new InvalidOperationException("El email ya está registrado para otro estudiante");

        var student = new Student
        {
            Name = studentCreateDto.Name,
            LastName = studentCreateDto.LastName,
            Age = studentCreateDto.Age,
            DocNumber = studentCreateDto.DocNumber,
            Email = studentCreateDto.Email,
            PhoneNumber = studentCreateDto.PhoneNumber,
            State = studentCreateDto.State,
            Semester = studentCreateDto.Semester,
            RegisteredAt = DateTime.UtcNow
        };

        // Asignar programas académicos si se proporcionan IDs
        if (studentCreateDto.AcademicProgramIds != null && studentCreateDto.AcademicProgramIds.Any())
        {
            student.AcademicPrograms = new List<AcademicProgram>();
            foreach (var programId in studentCreateDto.AcademicProgramIds)
            {
                var program = await _academicProgramRepository.GetAcademicProgramById(programId);
                if (program != null)
                    student.AcademicPrograms.Add(program);
            }
        }

        var createdStudent = await _studentRepository.CreateStudent(student);
        return MapToStudentResponseDto(createdStudent);
    }

    public async Task<StudentDtos.StudentResponseDto?> UpdateStudentAsync(StudentDtos.StudentUpdateDTO studentUpdateDto)
    {
        var existingStudent = await _studentRepository.GetStudentById(studentUpdateDto.Id);
        if (existingStudent == null)
            return null;

        existingStudent.Name = studentUpdateDto.Name;
        existingStudent.LastName = studentUpdateDto.LastName;
        existingStudent.Age = studentUpdateDto.Age;
        existingStudent.DocNumber = studentUpdateDto.DocNumber;
        existingStudent.Email = studentUpdateDto.Email;
        existingStudent.PhoneNumber = studentUpdateDto.PhoneNumber;
        existingStudent.State = studentUpdateDto.State;
        existingStudent.Semester = studentUpdateDto.Semester;

        // Actualizar programas académicos
        if (studentUpdateDto.AcademicProgramIds != null)
        {
            existingStudent.AcademicPrograms.Clear();
            foreach (var programId in studentUpdateDto.AcademicProgramIds)
            {
                var program = await _academicProgramRepository.GetAcademicProgramById(programId);
                if (program != null)
                    existingStudent.AcademicPrograms.Add(program);
            }
        }

        var updatedStudent = await _studentRepository.UpdateStudent(existingStudent);
        return updatedStudent == null ? null : MapToStudentResponseDto(updatedStudent);
    }

    public async Task<StudentDtos.StudentResponseDto?> DeleteStudentAsync(int id)
    {
        var deletedStudent = await _studentRepository.DeleteStudent(id);
        return deletedStudent == null ? null : MapToStudentResponseDto(deletedStudent);
    }

    // Mapeo de Entity a DTO
    private StudentDtos.StudentResponseDto MapToStudentResponseDto(Student student)
    {
        return new StudentDtos.StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            LastName = student.LastName,
            Age = student.Age,
            DocNumber = student.DocNumber,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            State = student.State,
            RegisteredAt = student.RegisteredAt,
            Semester = student.Semester,
            AcademicPrograms = student.AcademicPrograms?.Select(p => new AcademicProgramDtos.AcademicProgramResponseDto
            {
                Id = p.Id,
                ProgramName = p.ProgramName,
                Faculty = p.Faculty,
                Credits = p.Credits
            }).ToList() ?? new List<AcademicProgramDtos.AcademicProgramResponseDto>()
        };
    }
}