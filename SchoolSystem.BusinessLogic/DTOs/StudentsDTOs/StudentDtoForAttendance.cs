using SchoolSystem.Application.DTOs.AttendanceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.DTOs.StudentsDTOs
{
    public class StudentDtoForAttendance
    {
        public string? Name { get; set; }
        public List<AttendDtoForStudentInfo> AttendDtoForStudentInfo { get; set; }
    }
}
