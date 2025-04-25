using SchoolSystem.Application.DTOs.TeacherClassesDTOs;
using SchoolSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.Interfaces
{
  public interface ITeacherClassRepository:IGenericRepository<TeacherClass>
    {
        Task<TeacherClass?> GetByTeacheClassIdsAsync(int teacherId, int classId);
        Task<T_C_Dto_GetAll> GetTeacherClassDtoByTeacherAndClassIdsAsync(int teacherId, int classId);
    }
}
