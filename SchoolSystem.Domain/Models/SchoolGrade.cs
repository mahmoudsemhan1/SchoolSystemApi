using SchoolSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Models
{
    public class SchoolGrade
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
