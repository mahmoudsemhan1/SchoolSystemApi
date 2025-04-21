using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystem.Domain.Models
{
    public class TeacherSubject
    {
      
        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

 
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
    }
}
