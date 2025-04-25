using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Application.DTOs.TeachersDTOs;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystem.Application.Interfaces
{
    public interface ITeacherRepository:IGenericRepository<Teacher>
    {
        Task<TeacherDetailsDto> GetTeacherDetailsAsync(int teacherId);


    }
}
