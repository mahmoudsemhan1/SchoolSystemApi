using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace SchoolSystem.Infrastructure.Models;

public partial class Grade 
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? SubjectId { get; set; }

    public decimal? grade { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Subject? Subject { get; set; }
}
