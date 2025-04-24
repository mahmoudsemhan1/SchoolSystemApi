using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.DTOs.GradesDTOs
{
    public class GradeDetailDto
    {
        public string? StudentName { get; set; }
        public string? SubjectName { get; set; }
        public int Grade { get; set; }
    }
}
