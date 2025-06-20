using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.TeacherClassesDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Domain.Models;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // 🔐 تأمين كل العمليات على العلاقة Teacher-Classes
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

        /// ✅ Get all teacher-class relationships
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teacherClasses = await _unitOfWork.TeacherClasses.GetAllAsync(includeProperties: "Teacher,Class");
            var resultDto = _mapper.Map<IEnumerable<T_C_Dto_GetAll>>(teacherClasses);
            return Ok(resultDto);
        }

        /// ✅ Get specific teacher-class by both IDs
        [HttpGet("{teacherId}/{classId}")]
        public async Task<IActionResult> GetByIDs(int teacherId, int classId)
        {
            var teacherClass = await _unitOfWork.TeacherClass.GetByTeacheClassIdsAsync(teacherId, classId);

            if (teacherClass == null)
                return NotFound($"❌ No teacher-class relationship found for Teacher ID = {teacherId} and Class ID = {classId}");

            var resultDto = _mapper.Map<T_C_Dto_GetAll>(teacherClass);
            return Ok(resultDto);
        }

        /// ✅ Create a new teacher-class assignment
        [HttpPost]
        public async Task<IActionResult> CreateTeacherForClass([FromBody] T_C_ForCreate dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacherClass = _mapper.Map<TeacherClass>(dto);
            await _unitOfWork.TeacherClasses.AddAsync(teacherClass);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetByIDs), new { teacherId = teacherClass.TeacherId, classId = teacherClass.ClassId }, teacherClass);
        }

        /// ✅ Delete a teacher-class assignment
        [HttpDelete("{teacherId}/{classId}")]
        public async Task<IActionResult> Delete(int teacherId, int classId)
        {
            var deleted = await _teacherClassService.DeleteTeacherClassAsync(teacherId, classId);
            if (!deleted)
                return NotFound($"❌ No relation found to delete for Teacher ID = {teacherId} and Class ID = {classId}");

            return Ok($"✅ Relationship between Teacher ID = {teacherId} and Class ID = {classId} deleted.");
        }

        /// ✅ Get all classes assigned to a specific teacher
        [HttpGet("Teacher/{teacherId}")]
        public async Task<IActionResult> GetClassesByTeacherId(int teacherId)
        {
            var teacherClasses = await _unitOfWork.TeacherClasses.GetAllAsync(
                filter: tc => tc.TeacherId == teacherId,
                includeProperties: "Class"
            );

            if (teacherClasses == null || !teacherClasses.Any())
                return NotFound($"❌ No classes found for Teacher ID = {teacherId}");

            var resultDto = _mapper.Map<IEnumerable<T_C_Dto_GetAll>>(teacherClasses);
            return Ok(resultDto);
        }

        /// ✅ Get all teachers assigned to a specific class
        [HttpGet("Class/{classId}")]
        public async Task<IActionResult> GetTeachersByClassId(int classId)
        {
            var teacherClasses = await _unitOfWork.TeacherClasses.GetAllAsync(
                filter: tc => tc.ClassId == classId,
                includeProperties: "Teacher"
            );

            if (teacherClasses == null || !teacherClasses.Any())
                return NotFound($"❌ No teachers found for Class ID = {classId}");

            var resultDto = _mapper.Map<IEnumerable<T_C_Dto_GetAll>>(teacherClasses);
            return Ok(resultDto);
        }
    }
}
