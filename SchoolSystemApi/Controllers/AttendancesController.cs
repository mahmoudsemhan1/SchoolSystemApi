using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.AttendanceDTOs;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttendancesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAttendances()
        {
            var Attendances =await _unitOfWork.Attendances.GetAllAsync();
            return Ok(Attendances);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendByID(int id)
        {
            var studentAttend = await _unitOfWork.Attendances.GetByIdAsync(id);
            return Ok(studentAttend);
        }
      
        [HttpPost]
        public async Task<IActionResult> CreateAttendace([FromForm] AttendanceDto dto)
        {
            if (dto==null)
                return BadRequest();

            var AttendanceDto = _mapper.Map<Attendance>(dto);
            await _unitOfWork.Attendances.AddAsync(AttendanceDto);
            await _unitOfWork.CompleteAsync();

            return Ok(AttendanceDto);
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAttendance(int id, AttendanceDto dto)
        {
            var ExiteAttend = await _unitOfWork.Attendances.GetByIdAsync(id);
            if (ExiteAttend == null)
                return NotFound($"Attendance with ID {id} not found");

             _mapper.Map(dto, ExiteAttend);
            await _unitOfWork.CompleteAsync();

            return Ok(dto);
        
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var ExiteAttend = await _unitOfWork.Attendances.GetByIdAsync(id);
            if (ExiteAttend == null)
                return NotFound($"Attendance with ID {id} not found");

             _unitOfWork.Attendances.Delete(ExiteAttend);
             await _unitOfWork.CompleteAsync();

            return NoContent();

        }

    }
}
