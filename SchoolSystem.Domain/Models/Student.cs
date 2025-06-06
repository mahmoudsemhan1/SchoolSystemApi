using SchoolSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SchoolSystem.Infrastructure.Models;

public partial class Student
{
    public int StudentId { get; set; }
    public string? Name { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    public int? ClassId { get; set; }
 
    public string? ApplicationUserId { get; set; }
    public int? SchoolGradeID { get; set; } // FK
    public virtual SchoolGrade? SchoolGrade { get; set; }
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Class? Class { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public virtual ICollection<StudentGrade> Grades { get; set; } = new List<StudentGrade>();
}
