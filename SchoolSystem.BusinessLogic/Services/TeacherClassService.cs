using SchoolSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.Services
{
    public class TeacherClassService : ITeacherClassService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherClassService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteTeacherClassAsync(int teacherId, int classId)
        {
           
                // Get TeacherClass by composite key
                var teacherClass = await _unitOfWork.TeacherClass.GetByTeacheClassIdsAsync(teacherId, classId);

                // Delete TeacherClass and save changes
                 _unitOfWork.TeacherClasses.Delete(teacherClass);
                await _unitOfWork.CompleteAsync();

            return true;

        }
    }
}
