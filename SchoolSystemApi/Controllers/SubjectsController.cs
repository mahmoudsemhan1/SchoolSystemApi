using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.SubjectsDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SubjectsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _unitOfWork.Subjects.GetAllAsync();
            return Ok(subjects);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {

            var subject =await _unitOfWork.Subjects.GetByIdAsync(id);
            if (subject == null) return NotFound("Invalid ID , please Try anthor one ");

            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> CreatSubject([FromForm]SubjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //mapp between dto subject 
            var SubjectDto = _mapper.Map<Subject>(dto);
            await _unitOfWork.Subjects.AddAsync(SubjectDto);

            await _unitOfWork.CompleteAsync();

            return Ok(SubjectDto);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSubject(int id,SubjectDto dto)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (subject == null) return BadRequest();
             // 
             _mapper.Map(dto, subject);
            await _unitOfWork.CompleteAsync();
            //
            return Ok(subject);
        }
        //[HttpDelete]
        //public async Task<IActionResult> DeleteSubejct(int id)
        //{
        //    var subject = _unitOfWork.Subjects.GetByIdAsync(id);

        //    if (subject == null) return NotFound($"subject with ID {id} not found.");

        //    _unitOfWork.Subjects.Delete(subject);
        //    await _unitOfWork.CompleteAsync();

        //    return NoContent();
        //}


    }
}
