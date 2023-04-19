using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourAttendanceDTO
    {
        public string CheckpointName { get; set; }  
        public bool IsPresent { get; set; }

        public TourAttendanceDTO()
        {
            
        }

        public TourAttendanceDTO(string checkpointName, bool isPresent)
        {
            CheckpointName = checkpointName;
            IsPresent = isPresent;
        }
    }
}
