using EduTrack.Domain.Entities;

namespace EduTrack.Domain.Interfaces;

public interface IAcademicProgramRepository
{
    public Task<AcademicProgram?> GetAcademicProgramById(int id);
    public Task<AcademicProgram?> GetAcademicProgramByName(string name);
    public Task<IEnumerable<AcademicProgram>> GetAllAcademicPrograms();
    
    public Task<AcademicProgram> CreateAcademicProgram(AcademicProgram academicProgram);
    public Task<AcademicProgram?> UpdateAcademicProgram(AcademicProgram academicProgram);
    public Task<AcademicProgram?> DeleteAcademicProgram(int id);
}