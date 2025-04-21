using System;
using System.Collections.Generic;
using SchoolSystem.Domain.Models;

namespace SchoolSystem.Infrastructure.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
    public ICollection<ClassSubjects> ClassSubjects { get; set; } =new List<ClassSubjects>();
    public ICollection<TeacherSubject> TeacherSubjects { get; set; } 
}
