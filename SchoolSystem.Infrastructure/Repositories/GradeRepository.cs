using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class GradeRepository : GenericRepository<StudentGrade>, IGradeRepository
    {
        public GradeRepository(SchoolSystemDbContext context) : base(context)
        {
        }
    }
}
