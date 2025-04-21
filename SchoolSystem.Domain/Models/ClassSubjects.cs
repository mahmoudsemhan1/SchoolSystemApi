using SchoolSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Domain.Models
{
  public  class ClassSubjects
    {
        public int SubjectId { get; set; }
        public Subject subject { get; set; }

        public int ClassID { get; set; }

        //
        public Class Class { get; set; }
    }
}
