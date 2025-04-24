using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.DTOs.GradesDTOs
{
    public class CreateGradeDto
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public decimal Grade { get; set; }
    }
}
