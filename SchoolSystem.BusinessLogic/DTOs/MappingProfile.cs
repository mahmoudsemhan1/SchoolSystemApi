using AutoMapper;
using SchoolSystem.Application.DTOs.ClassesDtos;
using SchoolSystem.Application.DTOs.StudentsDTOs;
using SchoolSystem.Application.DTOs.SubjectsDTOs;
using SchoolSystem.Application.DTOs.TeachersDTOs;
using SchoolSystem.Application.DTOs.TimetableDtos;
using SchoolSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolSystem.Application.DTOs
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // student Mapp 
            CreateMap<Student, StudentDTo>().ReverseMap();
            CreateMap<StudentDTo, Student>()
                .ForMember(dest => dest.StudentId, opt => opt.Ignore()); // علشان ما نغيّرش الـ ID

            // teacher mapp
            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto, Teacher>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore()); // علشان ما نغيّرش الـ ID

            // Subject Mapp
            CreateMap<Subject, SubjectDto>();
            CreateMap<SubjectDto, Subject>()
                .ForMember(dest => dest.SubjectId, opt => opt.Ignore()); // علشان ما نغيّرش الـ ID

            // TimeTable Mapp 
            CreateMap<Timetable, TimetableDto>();

            // for classDto
            CreateMap<Class, ClassInformation>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.teachers, opt => opt.MapFrom(src =>
                    src.TeacherClasses.Select(tc => tc.teacher)))
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src =>
                    src.ClassSubjects.Select(cs => cs.subject)))
                .ForMember(dest => dest.timetableDtos, opt => opt.MapFrom(src => src.Timetables))
                .ReverseMap();  // Allows mapping back from ClassInformation to Class
        }
    }
}
