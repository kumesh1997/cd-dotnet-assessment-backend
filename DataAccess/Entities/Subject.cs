using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("subjects")]
public partial class Subject
{
    [Key]
    [Column("subject_id")]
    public int SubjectId { get; set; }

    [Column("subject_name")]
    [StringLength(100)]
    public string SubjectName { get; set; } = null!;

    [Column("subject_code")]
    [StringLength(50)]
    public string SubjectCode { get; set; } = null!;

    [Column("teacher_id")]
    [StringLength(100)]
    public string? TeacherId { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("Subjects")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
