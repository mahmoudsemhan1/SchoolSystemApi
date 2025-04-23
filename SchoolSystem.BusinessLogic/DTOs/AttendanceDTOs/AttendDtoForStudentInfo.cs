using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.DTOs.AttendanceDTOs
{
   public class AttendDtoForStudentInfo
    {
        public DateOnly? Date { get; set; }
        public string? Status { get; set; }
    }
}
