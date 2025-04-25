using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.Interfaces
{
    public interface ITeacherClassService
    {
        Task<bool> DeleteTeacherClassAsync(int teacherId, int classId);
    }
}
