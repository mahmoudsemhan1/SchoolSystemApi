using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Application.DTOs.ClassroomDtos;
using SchoolSystem.Application.DTOs.SubjectsDTOs;
using SchoolSystem.Application.DTOs.TimeSlotDtos;

namespace SchoolSystem.Application.DTOs.TimetableDtos
{
    public class TimetableDto
    {
        public string? DayOfWeek { get; set; }

        public SubjectDto Subject { get; set; } = new SubjectDto();
        public ClassroomDto Classroom { get; set; } = new ClassroomDto();
        public TimeSlotDto TimeSlot { get; set; } = new TimeSlotDto();
    }
}
