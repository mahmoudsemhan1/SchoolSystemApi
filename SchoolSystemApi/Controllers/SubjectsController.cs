using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.SubjectsDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // 🔒 حماية كل الإندبوينتس - الأدمن بس يقدر يدخل
    public class SubjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// ✅ Get all subjects
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _unitOfWork.Subjects.GetAllAsync();
            return Ok(subjects);
        }

        /// <summary>
        /// ✅ Get a subject by its ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (subject == null)
                return NotFound($"❌ Subject with ID {id} was not found.");

            return Ok(subject);
        }

        /// <summary>
        /// ✅ Create a new subject
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("❌ Invalid data. Please check the input and try again.");

            var subject = _mapper.Map<Subject>(dto);

            await _unitOfWork.Subjects.AddAsync(subject);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetSubjectById), new { id = subject.SubjectId }, subject);
        }

        /// <summary>
        /// ✅ Update an existing subject
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectDto dto)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (subject == null)
                return NotFound($"❌ Subject with ID {id} was not found.");

            _mapper.Map(dto, subject);
            await _unitOfWork.CompleteAsync();

            return Ok($"✅ Subject with ID {id} has been updated successfully.");
        }

        /// <summary>
        /// ✅ Delete a subject
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (subject == null)
                return NotFound($"❌ Subject with ID {id} was not found.");

            _unitOfWork.Subjects.Delete(subject);
            await _unitOfWork.CompleteAsync();

            return Ok($"✅ Subject with ID {id} has been deleted successfully.");
        }
    }
}
