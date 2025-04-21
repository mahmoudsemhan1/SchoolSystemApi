using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystem.Domain.Models
{
    public class TeacherClass
    {
        public int TeacherId { get; set; }
        public Teacher teacher { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }

    }
}
