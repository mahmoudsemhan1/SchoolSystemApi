using System;
using System.Collections.Generic;
using SchoolSystem.Domain.Models;

namespace SchoolSystem.Infrastructure.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
    public ICollection<TeacherClass> TeacherClasses { get; set; }
    public ICollection<TeacherSubject> TeacherSubjects { get; set; }

}
