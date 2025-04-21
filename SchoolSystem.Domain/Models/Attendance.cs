using System;
using System.Collections.Generic;

namespace SchoolSystem.Infrastructure.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? StudentId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Status { get; set; }

    public virtual Student? Student { get; set; }
}
