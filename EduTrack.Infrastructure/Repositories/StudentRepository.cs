using EduTrack.Domain.Entities;
using EduTrack.Domain.Interfaces;
using EduTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Student?> GetStudentById(int id)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student?> GetStudentByEmail(string email)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<IEnumerable<Student>> GetAllStudents()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> CreateStudent(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student?> UpdateStudent(Student student)
    {
        var existing = await _context.Students.FindAsync(student.Id);

        if (existing != null)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        return null;
    }

    public async Task<Student?> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }

        return null;
    }
}