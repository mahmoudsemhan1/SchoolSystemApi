using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.DTOs.AttendanceDTOs;
using SchoolSystem.Application.DTOs.StudentsDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly SchoolSystemDbContext _context;
        private readonly IMapper _mapper;
        public StudentRepository(SchoolSystemDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentDtoForAttendance> GetStudentAttendanceInfoAsync(string StudentName)
        {
            var AttendFor_Student = await _context.Students
                                   .Include(s => s.Attendances)
                                   .FirstOrDefaultAsync(s => s.Name == StudentName);

            if (AttendFor_Student == null) return null;

            var AttFStudDTO = _mapper.Map<StudentDtoForAttendance>(AttendFor_Student);


            return AttFStudDTO; 

        }
    }
}
