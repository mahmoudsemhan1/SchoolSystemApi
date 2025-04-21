using System;
using System.Collections.Generic;

namespace SchoolSystem.Infrastructure.Models;

public partial class Timetable
{
    public int TimetableId { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public string? DayOfWeek { get; set; }

    public int TimeSlotId { get; set; }

    public int ClassroomId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Classroom Classroom { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual TimeSlot TimeSlot { get; set; } = null!;
}
