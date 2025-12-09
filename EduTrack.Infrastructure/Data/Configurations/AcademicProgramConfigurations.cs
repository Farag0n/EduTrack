using EduTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduTrack.Infrastructure.Data.Configurations;

public class AcademicProgramConfigurations : IEntityTypeConfiguration<AcademicProgram>
{
    public void Configure(EntityTypeBuilder<AcademicProgram> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.ProgramName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Faculty)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Credits)
            .IsRequired();

        // Repetición opcional de la relación (no es necesaria)
        builder
            .HasMany(a => a.Students)
            .WithMany(s => s.AcademicPrograms)
            .UsingEntity(j => j.ToTable("StudentAcademicPrograms"));
    }
}