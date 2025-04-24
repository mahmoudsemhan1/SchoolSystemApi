using SchoolSystem.Application.DTOs.ClassesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.Interfaces
{
   public  interface IClassServices
    {
        Task<ClassInformation> GetClassDetails(int ClassId);

    }
}
