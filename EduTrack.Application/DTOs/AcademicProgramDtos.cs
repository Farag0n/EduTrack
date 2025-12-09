namespace EduTrack.Application.DTOs;

public class AcademicProgramDtos
{
    public class AcademicProgramCreateDto
    {
        public string ProgramName { get; set; }
        public string Faculty { get; set; }
        public int Credits { get; set; }
    }

    public class AcademicProgramUpdateDto
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public string Faculty { get; set; }
        public int Credits { get; set; }
    }

    public class AcademicProgramResponseDto
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public string Faculty { get; set; }
        public int Credits { get; set; }
    }
}