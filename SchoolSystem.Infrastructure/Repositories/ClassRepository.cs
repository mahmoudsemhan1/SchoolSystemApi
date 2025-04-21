using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.DTOs.ClassesDtos;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        public readonly SchoolSystemDbContext _context;
        private readonly IMapper _mapper;
        public ClassRepository(SchoolSystemDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ClassInformation> GetClassInformation(int classId)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Students)
                .Include(c => c.TeacherClasses)
                  .ThenInclude(tc => tc.teacher)
                .Include(c => c.ClassSubjects)
                   .ThenInclude(cs => cs.subject)
                .Include(c => c.Timetables)
                .FirstOrDefaultAsync(c => c.ClassId ==classId);
            if (classEntity == null)
            {
                throw new Exception("Class not found");
            }
           
            var classmapp=  _mapper.Map<ClassInformation>(classEntity);

            return classmapp;


        }
    }
}
