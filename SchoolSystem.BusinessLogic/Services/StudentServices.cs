using SchoolSystem.Application.DTOs.StudentsDTOs;
using SchoolSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.Services
{
    public class StudentServices : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<StudentDtoForAttendance> GetStudentAttendanceInfoAsync(string studentName)
        {
            return await _studentRepository.GetStudentAttendanceInfoAsync(studentName); 
        }
    }
}
