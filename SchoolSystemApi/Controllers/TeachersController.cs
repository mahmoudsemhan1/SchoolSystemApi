using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Application.DTOs.TeachersDTOs;
using SchoolSystem.Infrastructure.Models;
using SchoolSystem.Application.Services;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly TeacherService _teacherService;


        public TeachersController(IUnitOfWork unitOfWork, IMapper mapper, TeacherService teacherService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var Teachers =await _unitOfWork.Teachers.GetAllAsync();
            return Ok(Teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var Teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            return Ok(Teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TeacherDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacherdto = _mapper.Map<Teacher>(dto);
            await _unitOfWork.Teachers.AddAsync(teacherdto);

            await _unitOfWork.CompleteAsync();

            return Ok(teacherdto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,TeacherDto dto)
        {
            var teacher =await  _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher==null) return NotFound("Invalid ID , please Try anthor one ");


            _mapper.Map(dto, teacher);
            await _unitOfWork.CompleteAsync();
            //return the update object
            return Ok(teacher);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

            if (teacher == null) return NotFound($"Student with ID {id} not found.");

            _unitOfWork.Teachers.Delete(teacher);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        ///this enpoint for return the teacher with his subject and class )
        ///
        [HttpGet("TeacherDetails/{id}")]
        public async Task<IActionResult> GetTeacherDetails(int id)
        {
            var result = await _teacherService.GetTeacherDetails(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
