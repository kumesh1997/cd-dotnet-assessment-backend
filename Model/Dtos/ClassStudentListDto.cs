namespace Model.Dtos
{
    public class ClassStudentListDto
    {
        public string classId { get; set; }
        public string className { get; set; }
        public string? garde {  get; set; }
        public string? teacherId { get; set; }
        //public virtual ICollection<Student> Students { get; set; }
    }
}
