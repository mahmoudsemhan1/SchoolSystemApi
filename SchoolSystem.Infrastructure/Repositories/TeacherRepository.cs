using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.DTOs.TeachersDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly SchoolSystemDbContext _context;
        private readonly IMapper _mapper;
        public TeacherRepository(SchoolSystemDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TeacherDetailsDto> GetTeacherDetailsAsync(int teacherId)
        {
            var teacher = await _context.Teachers
                 .Include(t => t.TeacherSubjects)
                      .ThenInclude(ts => ts.Subject)
                 .Include(t => t.TeacherClasses)
                    .ThenInclude(tc => tc.Class)
                 .FirstOrDefaultAsync(t => t.TeacherId == teacherId);
            if (teacher == null) return null;


            var teaherdto = _mapper.Map<TeacherDetailsDto>(teacher);
            return teaherdto;


        }
    }
}
