using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Application.DTOs.ClassesDtos;
using SchoolSystem.Application.Interfaces;

namespace SchoolSystem.Application.Services
{
    public class ClassServices:IClassServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClassInformation> GetClassDetails(int ClassId)
        {
            return  await _unitOfWork.classRepository.GetClassInformation(ClassId);
        }
    }
}
