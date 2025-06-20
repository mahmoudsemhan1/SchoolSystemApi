using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Application.DTOs;
using SchoolSystem.Infrastructure.Models;
using SchoolSystem.Application.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Identity;
using SchoolSystem.Domain.Models;
using SchoolSystem.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Student")]
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
     

        public StudentsController(IUnitOfWork unitOfWork, IMapper mapper, IStudentService studentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _studentService = studentService;
            
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetStudents()
        {
            var Students = await _unitOfWork.Students.GetAllAsync();
            return Ok(Students);
        }
        //GetByID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetStudent(int id)
        {

            var Student = await _unitOfWork.Students.GetByIdAsync(id);
            if (Student == null) return NotFound("Invalid ID , please Try anthor one ");

            return Ok(Student);
        }
        /// <summary>
        /// Endpoint get student with the attendances of him 
        /// </summary>
        /// <param name="studentName"></param>
        /// <returns></returns>
        [HttpGet("attendance")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetStudentAttendance([FromQuery] string? studentName)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue("UserId");

            if (userRole == "Student")
            {
                // Students are not allowed to specify another student's name
                if (!string.IsNullOrWhiteSpace(studentName))
                    return Forbid("You are not allowed to access other students' data.");

                // Get the student's real name from the database using their user ID
                var student = await _unitOfWork.Students.GetByIdAsync(int.Parse(userId!));
                if (student == null)
                    return NotFound("Student not found.");

                studentName = student.Name; // Use the student's own name for the query
            }
            else if (userRole == "Admin")
            {
                // Admins must provide a student name to query attendance
                if (string.IsNullOrWhiteSpace(studentName))
                    return BadRequest("Student name is required.");
            }

            // Retrieve the student's attendance information
            var studentAttendance = await _studentService.GetStudentAttendanceInfoAsync(studentName!);

            if (studentAttendance == null)
                return NotFound("Student not found.");

            return Ok(studentAttendance);
        }

        // Add Student
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([FromForm] StudentDTo dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = _mapper.Map<Student>(dto);
            await _unitOfWork.Students.AddAsync(student);
    
            await _unitOfWork.CompleteAsync();

            return Ok(student);
        }
        //Update Student
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id, StudentDTo studentDTo )
        {
            var Student = await _unitOfWork.Students.GetByIdAsync(id);

            if (Student == null) return NotFound($"Student with ID {id} not found.");

            _mapper.Map(studentDTo, Student);
            await _unitOfWork.CompleteAsync();

            return Ok(Student);

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var Student = await _unitOfWork.Students.GetByIdAsync(id);

            if (Student == null) return NotFound($"Student with ID {id} not found.");

             _unitOfWork.Students.Delete(Student);
            await _unitOfWork.CompleteAsync();


            return NoContent();
        }

        [HttpGet("MyProfile")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyProfile()
        {
            var studentIdStr = User.FindFirstValue("UserId");
            if (!int.TryParse(studentIdStr, out int studentId))
                return BadRequest("Invalid student ID.");

            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }

    }
}
