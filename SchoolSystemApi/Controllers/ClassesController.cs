using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.ClassesDtos;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Application.Services;
using SchoolSystem.Infrastructure.Models;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ClassServices _classServices;

        public ClassesController(IUnitOfWork unitOfWork, IMapper mapper, ClassServices classServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _classServices = classServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var Classes=await _unitOfWork.Classes.GetAllAsync();
            return Ok(Classes);
        }
        /// <summary>
        /// this endpoint for get class and the related data (students , subjects, teachers and the timeTable)
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [HttpGet("/InFormation/{ClassId}")]
        public  async Task<IActionResult> GetClassInformation(int ClassId)
        {
            var ClassInfo= await _classServices.GetClassDetails(ClassId);
             if(ClassInfo == null) 
                return NotFound(_classServices.GetClassDetails(ClassId));

            return Ok(ClassInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var class_=await _unitOfWork.Classes.GetByIdAsync(id);

            if (class_ == null)
                return NotFound("Invalid ID , please Try anthor one ");
            return Ok(class_);
        }

        [HttpPost]
        public async Task<IActionResult> CreatClass(ClassesDto dto)
        {
            if(dto == null)
                return BadRequest();
            // mapp between class and classDto 
            var classdto=_mapper.Map<Class>(dto);
            //add Classdto to table class in database 
           await _unitOfWork.Classes.AddAsync(classdto);
            //Save the data that insert in the table 
           await _unitOfWork.CompleteAsync();
           return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClass(int id , ClassesDto dto)
        {
            var existClass = _unitOfWork.Classes.GetByIdAsync(id);

            if(existClass == null)
                return NotFound("Invalid ID , please Try anthor one ");

            // Map the updated values from the DTO into the existing entity object
            // This updates only matching properties without creating a new instance
            await _mapper.Map(dto,existClass);

            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        
    }
}
