using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.TeachersDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // 🔐 تأمين كل العمليات للإدمن فقط
    public class TeachersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITeacherServices _teacherService;

        public TeachersController(IUnitOfWork unitOfWork, IMapper mapper, ITeacherServices teacherService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _teacherService = teacherService;
        }

        /// <summary>✅ Get all teachers</summary>
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _unitOfWork.Teachers.GetAllAsync();
            return Ok(teachers);
        }

        /// <summary>✅ Get teacher by ID</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
                return NotFound($"❌ Teacher with ID {id} not found.");

            return Ok(teacher);
        }

        /// <summary>✅ Create a new teacher</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TeacherDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("❌ Invalid teacher data.");

            var teacher = _mapper.Map<Teacher>(dto);
            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.TeacherId }, teacher);
        }

        /// <summary>✅ Update teacher info</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] TeacherDto dto)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
                return NotFound($"❌ Teacher with ID {id} not found.");

            _mapper.Map(dto, teacher);
            await _unitOfWork.CompleteAsync();

            return Ok($"✅ Teacher with ID {id} updated successfully.");
        }

        /// <summary>✅ Delete a teacher</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
                return NotFound($"❌ Teacher with ID {id} not found.");

            _unitOfWork.Teachers.Delete(teacher);
            await _unitOfWork.CompleteAsync();

            return Ok($"✅ Teacher with ID {id} deleted successfully.");
        }

        /// <summary>✅ Get teacher with subject and class</summary>
        [HttpGet("TeacherDetails/{id}")]
        public async Task<IActionResult> GetTeacherDetails(int id)
        {
            var result = await _teacherService.GetTeacherDetails(id);
            if (result == null)
                return NotFound($"❌ No details found for teacher ID {id}.");

            return Ok(result);
        }
    }
}
