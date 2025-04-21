using System;
using System.Collections.Generic;

namespace SchoolSystem.Infrastructure.Models;

public partial class Classroom
{
    public int ClassroomId { get; set; }

    public string? RoomNumber { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
