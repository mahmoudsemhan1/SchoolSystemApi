using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.ClassroomDtos;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;
using System.Threading.Tasks;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomesController : ControllerBase
    {
        private readonly  IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassroomesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAllRoomes()
        {
            var Roomes = await _unitOfWork.ClassRoomes.GetAllAsync();
            return Ok(Roomes);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetRoomBYId(int id)
        {
            var Room = await _unitOfWork.ClassRoomes.GetByIdAsync(id);
            return Ok(Room);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public  async Task<IActionResult> CreateRoom( ClassroomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("some fildes is empty");

            var RoomDto = _mapper.Map<Classroom>(dto);
           await _unitOfWork.ClassRoomes.AddAsync(RoomDto);
            await _unitOfWork.CompleteAsync();

            return Ok(RoomDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(int id,ClassroomDto dto)
        {
            var existRoom =await _unitOfWork.ClassRoomes.GetByIdAsync(id);
            if (existRoom == null)
                return NotFound($"Room with ID {id} not found ,try anothr id.");

            _mapper.Map(dto, existRoom);
           await _unitOfWork.CompleteAsync();

            return Ok(dto);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _unitOfWork.ClassRoomes.GetByIdAsync(id);
            if (room == null)
                return NotFound();

            _unitOfWork.ClassRoomes.Delete(room);
            await _unitOfWork.CompleteAsync();

            return NoContent();


        }

    }
}
