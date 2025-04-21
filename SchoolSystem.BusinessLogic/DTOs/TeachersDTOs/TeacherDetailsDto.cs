using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.DTOs.TeachersDTOs
{
    public class TeacherDetailsDto
    {
        public string TeacherName { get; set; }
        public List<string> Subjects { get; set; }
        public List<string> Classes { get; set; }
    }
}
