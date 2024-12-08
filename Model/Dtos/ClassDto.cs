namespace Model.Dtos
{
    public class ClassDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public string? Grade { get; set; }
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();
    }
}
