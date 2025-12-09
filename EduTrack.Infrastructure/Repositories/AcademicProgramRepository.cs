using EduTrack.Domain.Entities;
using EduTrack.Domain.Interfaces;
using EduTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Infrastructure.Repositories;

public class AcademicProgramRepository : IAcademicProgramRepository
{
    private readonly AppDbContext _context;

    public AcademicProgramRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AcademicProgram?> GetAcademicProgramById(int id)
    {
        return await _context.AcademicPrograms
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<AcademicProgram?> GetAcademicProgramByName(string name)
    {
        return await _context.AcademicPrograms
            .FirstOrDefaultAsync(a => a.ProgramName == name);
    }

    public async Task<IEnumerable<AcademicProgram>> GetAllAcademicPrograms()
    {
        return await _context.AcademicPrograms.ToListAsync();
    }

    public async Task<AcademicProgram> CreateAcademicProgram(AcademicProgram academicProgram)
    {
        await _context.AcademicPrograms.AddAsync(academicProgram);
        await _context.SaveChangesAsync();
        return academicProgram;
    }

    public async Task<AcademicProgram?> UpdateAcademicProgram(AcademicProgram academicProgram)
    {
        var existing = await _context.AcademicPrograms.FindAsync(academicProgram.Id);

        if (existing != null)
        {
            _context.AcademicPrograms.Update(academicProgram);
            await _context.SaveChangesAsync();
            return academicProgram;
        }

        return null;
    }

    public async Task<AcademicProgram?> DeleteAcademicProgram(int id)
    {
        var academicProgram = await _context.AcademicPrograms.FindAsync(id);

        if (academicProgram != null)
        {
            _context.AcademicPrograms.Remove(academicProgram);
            await _context.SaveChangesAsync();
            return academicProgram;
        }

        return null;
    }
}