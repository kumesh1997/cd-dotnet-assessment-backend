using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("students")]
public partial class Student
{
    [Key]
    [Column("student_id")]
    public int StudentId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("age")]
    public int Age { get; set; }

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [Column("class_id")]
    public int? ClassId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("Students")]
    public virtual Class? Class { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Students")]
    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
