using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.GradesDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Domain.Common;
using SchoolSystem.Infrastructure.Models;
using System.Security.Claims;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Teacher,Student")]
    public class GradesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GradesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAllGrades()
        {
            var grades = await _unitOfWork.Grades.GetAllAsync(includeProperties: "Student,Subject");
            var gradesDto = _mapper.Map<IEnumerable<GradeDetailDto>>(grades);
            return Ok(gradesDto);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetGrade(int id)
        {
            var grade = await _unitOfWork.Grades.GetFirstOrDefaultAsync(g => g.Id == id, includeProperties: "Student,Subject");
            if (grade == null)
                return NotFound();

            var gradedto = _mapper.Map<GradeDetailDto>(grade);
            return Ok(gradedto);
        }

        [HttpGet("Student/{name}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetGrade(string name)
        {
            var grade = await _unitOfWork.Grades.GetAllAsync(
                g => g.Student.Name.ToLower().Contains(name.ToLower()),
                includeProperties: "Student,Subject");

            if (grade == null)
                return NotFound();

            var gradedto = _mapper.Map<IEnumerable<GradeDetailDto>>(grade);
            return Ok(gradedto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateGradeDto dto)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var teacherId = User.FindFirstValue("UserId");

            var student = await _unitOfWork.Students.GetByIdAsync(dto.StudentId);
            var subject = await _unitOfWork.Subjects.GetFirstOrDefaultAsync(
                s => s.SubjectId == dto.SubjectId,
                includeProperties: "TeacherSubjects");

            if (student == null || subject == null)
                return BadRequest("Student or Subject not found.");

            if (userRole == "Teacher" && !subject.TeacherSubjects.Any(ts => ts.TeacherId.ToString() == teacherId))
                return Forbid("You can only assign grades for your own subjects.");

            var newGrade = _mapper.Map<StudentGrade>(dto);
            await _unitOfWork.Grades.AddAsync(newGrade);
            await _unitOfWork.CompleteAsync();

            return Ok(newGrade);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] CreateGradeDto dto)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var teacherId = User.FindFirstValue("UserId");
            var oldGrade = await _unitOfWork.Grades.GetByIdAsync(id);

            if (oldGrade == null)
                return NotFound();

            var subject = await _unitOfWork.Subjects.GetFirstOrDefaultAsync(
                s => s.SubjectId == dto.SubjectId,
                includeProperties: "TeacherSubjects");

            if (subject == null)
                return BadRequest("Subject not found.");

            if (userRole == "Teacher" && !subject.TeacherSubjects.Any(ts => ts.TeacherId.ToString() == teacherId))
                return Forbid("You can only update grades for your own subjects.");

            _mapper.Map(dto, oldGrade);
            await _unitOfWork.CompleteAsync();

            return Ok(oldGrade);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var teacherId = User.FindFirstValue("UserId");

            var grade = await _unitOfWork.Grades.GetFirstOrDefaultAsync(
                g => g.Id == id,
                includeProperties: "Subject.TeacherSubjects");

            if (grade == null)
                return NotFound();

            if (userRole == "Teacher" && !grade.Subject.TeacherSubjects.Any(ts => ts.TeacherId.ToString() == teacherId))
                return Forbid("You can only delete grades for your own subjects.");

            _unitOfWork.Grades.Delete(grade);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }


        [HttpGet("MyGrades")]
         public async Task<IActionResult> GetMyGrades()
        {
            var userIdStr = User.FindFirstValue("UserId");
            if (!int.TryParse(userIdStr, out int studentId))
                return BadRequest("Invalid student ID.");

            var grades = await _unitOfWork.Grades.GetAllAsync(
                g => g.StudentId == studentId,
                includeProperties: "Subject,Student");

            var gradesDto = _mapper.Map<IEnumerable<GradeDetailDto>>(grades);
            return Ok(gradesDto);
        }

    }
}
