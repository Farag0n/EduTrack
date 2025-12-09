using EduTrack.Domain.Entities;

namespace EduTrack.Domain.Interfaces;

public interface IStudentRepository
{
    public Task<Student?> GetStudentById(int id);
    public Task<Student?> GetStudentByEmail(string email);
    public Task<IEnumerable<Student>> GetAllStudents();
    
    public Task<Student> CreateStudent(Student student);
    public Task<Student?> UpdateStudent(Student student);
    public Task<Student?> DeleteStudent(int id);
}