﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Entities;

[Table("classes")]
public partial class Class
{
    [Key]
    [Column("class_id")]
    public int ClassId { get; set; }

    [Column("class_name")]
    [StringLength(100)]
    public string ClassName { get; set; } = null!;

    [Column("grade")]
    [StringLength(50)]
    public string? Grade { get; set; }

    [Column("teacher_id")]
    [StringLength(100)]
    public string? TeacherId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [JsonIgnore]
    [InverseProperty("Class")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
