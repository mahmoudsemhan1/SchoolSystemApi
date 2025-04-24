using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using SchoolSystem.Application.DTOs.GradesDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly IGradeServices _gradeServices;

        public GradesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        //    _gradeServices = gradeServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGrades()
        {
            //get grade with subject and student
            var grades = await _unitOfWork.Grades.GetAllAsync(includeProperties:"Student,Subject");
            var gradesDto = _mapper.Map<IEnumerable<GradeDetailDto>>(grades);
            return Ok(gradesDto);
        }
        /// <summary>
        /// get studen name and subject and the grade of this subject  by using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrade(int id)
        {
            //get grade with subject and student
            var grade = await _unitOfWork.Grades.GetFirstOrDefaultAsync(g=>g.Id==id,
                includeProperties: "Student,Subject"
                );

            if (grade == null)
                return NotFound();

            var gradedto = _mapper.Map<GradeDetailDto>(grade);
            return Ok(gradedto);
        }
        /// <summary>
        /// get student name and subjects name and grades by=> using the name of student
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("Student/{name}")]
        public async Task<IActionResult> GetGrade(string name)
        {
            //get grade with subject and student
            var grade = await _unitOfWork.Grades.GetAllAsync(
                  g => g.Student.Name.ToLower().Contains(name.ToLower()), 
                includeProperties: "Student,Subject"
                );

            if (grade == null)
                return NotFound();

            var gradedto = _mapper.Map<IEnumerable<GradeDetailDto>>(grade);
            return Ok(gradedto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGradeDto dto)
        {
            // first check are student or subject is exist or not 
            var student = await _unitOfWork.Students.GetByIdAsync(dto.StudentId);
            var subject = await _unitOfWork.Subjects.GetByIdAsync(dto.SubjectId);
            if (student == null || subject == null)
                return BadRequest("Student or Subject not found.");

            var Newgrad = _mapper.Map<Grade>(dto);

            await _unitOfWork.Grades.AddAsync(Newgrad);
            await _unitOfWork.CompleteAsync();

            return Ok(Newgrad);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGrade(int Id, [FromBody] CreateGradeDto dto)
        {
            var oldGrade =await _unitOfWork.Grades.GetByIdAsync(Id);
            if (oldGrade == null)
                return NotFound();

            //var subject = await _unitOfWork.Subjects.GetByIdAsync(dto.SubjectId);
            //var Student = await _unitOfWork.Students.GetByIdAsync(dto.StudentId);

            //if (Student == null || subject == null)
            //    return BadRequest("Student or Subject not found.");

            _mapper.Map(dto, oldGrade);
            await _unitOfWork.CompleteAsync();

            return Ok(oldGrade);


        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var grad = await _unitOfWork.Grades.GetByIdAsync(Id);
            if (grad == null)
                return NotFound();
             _unitOfWork.Grades.Delete(grad);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

    }
}
