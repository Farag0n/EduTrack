using Microsoft.EntityFrameworkCore;
using EduTrack.Domain.Entities;
using EduTrack.Infrastructure.Data.Configurations;

namespace EduTrack.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Student> Students { get; set; }
    public DbSet<AcademicProgram> AcademicPrograms { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Aplica las configuraciones Fluent API desde la carpeta Configurations
        modelBuilder.ApplyConfiguration(new StudentConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new AcademicProgramConfigurations());
    }
}