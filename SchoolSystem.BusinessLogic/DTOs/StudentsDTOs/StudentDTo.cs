using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.DTOs.StudentsDTOs
{
   public class StudentDTo
    {
        public string? Name { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public int? ClassId { get; set; }
    }
}
