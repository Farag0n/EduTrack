using EduTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduTrack.Infrastructure.Data.Configurations;

public class StudentConfigurations : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(50);
        
        //agregar un valor unico
        builder.HasIndex(s => s.DocNumber).IsUnique();
        builder.Property(s => s.DocNumber)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.PhoneNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.State).IsRequired();
        builder.Property(s => s.RegisteredAt).IsRequired();

        // 👉 Configuración de muchos a muchos
        builder
            .HasMany(s => s.AcademicPrograms)
            .WithMany(a => a.Students)
            .UsingEntity(j => j.ToTable("StudentAcademicPrograms"));
    }
}
