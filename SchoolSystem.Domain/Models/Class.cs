using System;
using System.Collections.Generic;
using System.Diagnostics;
using SchoolSystem.Domain.Models;

namespace SchoolSystem.Infrastructure.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }
    public int? SchoolGradeID { get; set; } // FK
    public virtual SchoolGrade? SchoolGrade { get; set; }
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();

    public ICollection<ClassSubjects> ClassSubjects { get; set; } = new List<ClassSubjects>();
    public ICollection<TeacherClass> TeacherClasses { get; set; } = new List<TeacherClass>();

}
