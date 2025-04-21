using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Application.DTOs.StudentsDTOs;
using SchoolSystem.Application.DTOs.SubjectsDTOs;
using SchoolSystem.Application.DTOs.TeachersDTOs;
using SchoolSystem.Application.DTOs.TimetableDtos;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystem.Application.DTOs.ClassesDtos
{
    public class ClassInformation
    {
        public string? ClassName { get; set; }

        public List<StudentDTo> Students { get; set; } = new();
        public List<TeacherDto> teachers { get; set; } = new();
        public List<SubjectDto> Subjects { get; set; } = new();

        public List<TimetableDto> timetableDtos { get; set; } = new();

    }
}
