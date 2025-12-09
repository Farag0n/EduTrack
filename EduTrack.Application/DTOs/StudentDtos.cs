using EduTrack.Domain.Enums;

namespace EduTrack.Application.DTOs;

public class StudentDtos
{
    public class StudentCreateDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string DocNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public StudentState State { get; set; }
        public int Semester { get; set; }
        public List<int> AcademicProgramIds { get; set; } = new();
    }

    public class StudentUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string DocNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public StudentState State { get; set; }
        public int Semester { get; set; }
        public List<int> AcademicProgramIds { get; set; } = new();
    }

    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string DocNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public StudentState State { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int Semester { get; set; }
        public List<AcademicProgramDtos.AcademicProgramResponseDto> AcademicPrograms { get; set; } = new();
    }
}