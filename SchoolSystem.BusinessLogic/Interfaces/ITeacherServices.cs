using SchoolSystem.Application.DTOs.TeachersDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.Interfaces
{
   public interface ITeacherServices
    {
        Task<TeacherDetailsDto> GetTeacherDetails(int teacherId);
    }
}
