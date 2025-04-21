using System;
using System.Collections.Generic;

namespace SchoolSystem.Infrastructure.Models;

public partial class TimeSlot
{
    public int TimeSlotId { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
