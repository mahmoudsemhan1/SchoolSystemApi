using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSystem.Application.DTOs.SubjectsDTOs;

namespace SchoolSystem.Application.DTOs.TimeSlotDtos
{
    public class TimeSlotDto
    {
        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }
        public List<SubjectDto> Subjects { get; set; } = new();
    }
}
