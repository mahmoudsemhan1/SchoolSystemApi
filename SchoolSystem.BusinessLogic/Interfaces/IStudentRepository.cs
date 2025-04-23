using SchoolSystem.Application.DTOs.StudentsDTOs;
using SchoolSystem.Application.DTOs.SubjectsDTOs;
using SchoolSystem.Application.DTOs.TeachersDTOs;
using SchoolSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.Interfaces
{
    public interface IStudentRepository: IGenericRepository<Student>
    {
        Task<StudentDtoForAttendance> GetStudentAttendanceInfoAsync(string StudentName);
    }
}
