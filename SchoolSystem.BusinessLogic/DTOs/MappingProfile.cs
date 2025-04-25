using AutoMapper;
using SchoolSystem.Application.DTOs.AttendanceDTOs;
using SchoolSystem.Application.DTOs.ClassesDtos;
using SchoolSystem.Application.DTOs.ClassroomDtos;
using SchoolSystem.Application.DTOs.GradesDTOs;
using SchoolSystem.Application.DTOs.StudentsDTOs;
using SchoolSystem.Application.DTOs.SubjectsDTOs;
using SchoolSystem.Application.DTOs.TeacherClassesDTOs;
using SchoolSystem.Application.DTOs.TeachersDTOs;
using SchoolSystem.Application.DTOs.TimetableDtos;
using SchoolSystem.Domain.Models;
using SchoolSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolSystem.Application.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // student Mapp 
            CreateMap<Student, StudentDTo>().ReverseMap();
            CreateMap<StudentDTo, Student>()
                .ForMember(dest => dest.StudentId, opt => opt.Ignore()); // علشان ما نغيّرش الـ ID
            //for Attendance 
            CreateMap<Student, StudentDtoForAttendance>()
                 .ForMember(dest => dest.AttendDtoForStudentInfo,
               opt => opt.MapFrom(src => src.Attendances));

            // teacher mapp
            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto, Teacher>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore()); // علشان ما نغيّرش الـ ID
            CreateMap<Teacher, TeacherDetailsDto>()
                 .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src =>
                     src.TeacherSubjects.Select(ts => ts.Subject.SubjectName).ToList()))
                 .ForMember(dest => dest.Classes, opt => opt.MapFrom(src =>
                     src.TeacherClasses.Select(tc => tc.Class.ClassName).ToList()));

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

            //for ClassRoom 
            CreateMap<Classroom, ClassroomDto>().ReverseMap();
            //for Attendance
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            CreateMap<Attendance, AttendDtoForStudentInfo>();
            //for Grade 
            CreateMap<Grade, CreateGradeDto>().ReverseMap();
             CreateMap<Grade, GradeDetailDto>()
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
            .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName))
            .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.grade));
            //teacheClass
            CreateMap<TeacherClass, T_C_Dto_GetAll>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src=>src.teacher.Name))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src=>src.Class.ClassName));
            CreateMap<TeacherClass, T_C_ForCreate>().ReverseMap();

           
        }
    }
}
