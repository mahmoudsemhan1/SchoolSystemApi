using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Application.DTOs.StudentsDTOs
{
    public class RegisterStudentDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }  

        public int GradeId { get; set; }
        public int ClassId { get; set; }
    }
}
