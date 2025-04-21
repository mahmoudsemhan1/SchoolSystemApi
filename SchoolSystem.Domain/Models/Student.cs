using System;
using System.Collections.Generic;

namespace SchoolSystem.Infrastructure.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Name { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? ClassId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Class? Class { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
