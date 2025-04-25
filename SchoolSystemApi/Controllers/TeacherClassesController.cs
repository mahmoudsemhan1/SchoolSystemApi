using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.TeacherClassesDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Domain.Models;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherClassesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITeacherClassService _teacherClassService;
        public TeacherClassesController(IUnitOfWork unitOfWork, IMapper mapper, ITeacherClassService teacherClassService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _teacherClassService = teacherClassService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var T_C =await  _unitOfWork.TeacherClasses.GetAllAsync(includeProperties: "teacher,Class");
            var T_CDto = _mapper.Map<IEnumerable<T_C_Dto_GetAll>>(T_C);
            return Ok(T_CDto);
        }
        [HttpGet("{teacherId}/{classId}")]
        public async Task<IActionResult> GetByIDs(int teacherId,int classId)
        {
            var TeacherClass =await _unitOfWork.TeacherClass.GetByTeacheClassIdsAsync(teacherId, classId);

            if (TeacherClass == null)
                return NotFound();
            var T_CDto = _mapper.Map<T_C_Dto_GetAll>(TeacherClass);

            return Ok(T_CDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacherforClass(T_C_ForCreate dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var TcDto = _mapper.Map<TeacherClass>(dto);
            await _unitOfWork.TeacherClasses.AddAsync(TcDto);

            await _unitOfWork.CompleteAsync();

            return Ok(TcDto);

        }

        [HttpDelete("{teacherId}/{classId}")]
        public async Task<IActionResult> Delete(int teacherId, int classId)
        {
            var teacherClass = await _teacherClassService.DeleteTeacherClassAsync(teacherId,classId);
            if (!teacherClass)
            {
                return NotFound();  // Return 404 if the record is not found
            }
            return NoContent();
        }

    }
}
