using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Application.DTOs.ClassesDtos;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystem.Application.Interfaces
{
    public interface IClassRepository : IGenericRepository<Class>
    {
       Task <ClassInformation> GetClassInformation(int ClassId);
    }
}
