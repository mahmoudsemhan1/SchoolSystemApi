using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.DTOs.TeacherClassesDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Domain.Models;
using SchoolSystem.Infrastructure.Exceptions;
using SchoolSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class TeacherClassRepository : GenericRepository<TeacherClass>, ITeacherClassRepository
    {
        private readonly SchoolSystemDbContext _context;
        private readonly IMapper _mapper;
        public TeacherClassRepository(SchoolSystemDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        // Method to get TeacherClass by composite key
        public async Task<TeacherClass?> GetByTeacheClassIdsAsync(int teacherId, int classId)
        {
            var TeacherClass= await _context.TeacherClasses
                .Include(tc=>tc.teacher)
                .Include(tc=>tc.Class)
                .FirstOrDefaultAsync(tc=>tc.TeacherId==teacherId && tc.ClassId==classId );
            if (TeacherClass == null)
            {
                // Throwing custom NotFoundException
                throw new NotFoundException($"TeacherClass with TeacherId {teacherId} and ClassId {classId} not found.");
            }
            return TeacherClass;
        }

        public async Task<T_C_Dto_GetAll> GetTeacherClassDtoByTeacherAndClassIdsAsync(int teacherId, int classId)
        {
            var teacherClass = await GetByTeacheClassIdsAsync(teacherId, classId);
            if (teacherClass == null)
                throw new NotFoundException($"TeacherClass with TeacherId {teacherId} and ClassId {classId} not found.");

            // Use AutoMapper to project to DTO
            return _mapper.Map<T_C_Dto_GetAll>(teacherClass);
        }
    }
}
