using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Application.DTOs.TeachersDTOs;
using SchoolSystem.Application.Interfaces;

namespace SchoolSystem.Application.Services
{
    public class TeacherService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TeacherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TeacherDetailsDto> GetTeacherDetails(int teacherId)
        {
            return await _unitOfWork.TeacherRepository.GetTeacherDetailsAsync(teacherId);
        }
    }
}
