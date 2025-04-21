using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Application.DTOs;
using SchoolSystem.Infrastructure.Models;
using SchoolSystem.Application.DTOs.StudentsDTOs;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

    }
}
