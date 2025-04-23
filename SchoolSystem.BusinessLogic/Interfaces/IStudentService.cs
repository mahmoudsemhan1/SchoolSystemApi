using SchoolSystem.Application.DTOs.StudentsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.Interfaces
{
  public  interface IStudentService
    {
        Task<StudentDtoForAttendance> GetStudentAttendanceInfoAsync(string studentName);

    }
}
