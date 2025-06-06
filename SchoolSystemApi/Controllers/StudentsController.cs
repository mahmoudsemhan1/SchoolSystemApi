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

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole>  _roleManager ;

        public StudentsController(IUnitOfWork unitOfWork, IMapper mapper, IStudentService studentService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _studentService = studentService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var Students = await _unitOfWork.Students.GetAllAsync();
            return Ok(Students);
        }
        //GetByID
        [HttpGet("{id}")]
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
        public async Task<IActionResult> GetStudentAttendance([FromQuery] string studentName)
        {
            if (string.IsNullOrWhiteSpace(studentName))
                return BadRequest("Student name is required.");

            var studentAttendance = await _studentService.GetStudentAttendanceInfoAsync(studentName);

            if (studentAttendance == null)
                return NotFound("Student not found.");

            return Ok(studentAttendance);
        }
        // Add Student
        [HttpPost]
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
        public async Task<IActionResult> Update(int id, StudentDTo studentDTo )
        {
            var Student = await _unitOfWork.Students.GetByIdAsync(id);

            if (Student == null) return NotFound($"Student with ID {id} not found.");

            _mapper.Map(studentDTo, Student);
            await _unitOfWork.CompleteAsync();

            return Ok(Student);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var Student = await _unitOfWork.Students.GetByIdAsync(id);

            if (Student == null) return NotFound($"Student with ID {id} not found.");

             _unitOfWork.Students.Delete(Student);
            await _unitOfWork.CompleteAsync();


            return NoContent();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterStudentDto model)
        {

            var user = _mapper.Map<ApplicationUser>(model);
     
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            // Assign Role
            if (!await _roleManager.RoleExistsAsync(AppRoles.Student))
                await _roleManager.CreateAsync(new IdentityRole(AppRoles.Student));

            await _userManager.AddToRoleAsync(user, AppRoles.Student);
            // 3. تحويل DTO إلى Student
            var student = _mapper.Map<Student>(model);
            student.ApplicationUserId = user.Id;

            await _unitOfWork.CompleteAsync();

            return Ok("User registered successfully!");

        }

    }
}
