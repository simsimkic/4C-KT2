using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace InitialProject.Model
{
    public class TourReservation
    {
        [Key]
        public int TourReservationId { get; set; }

        public int TouristsNumber { get; set; }

        public bool Attendance { get; set; }

        public List<TouristNotifications> Notifications { get; set; }

        public TourReservation()
        {
                
        }

        public TourReservation(int touristsNumber)
        {
            TouristsNumber = touristsNumber;
            Attendance = false;
            Notifications = new List<TouristNotifications>();
        }

        public TourReservation(int touristsNumber, bool attendance)
        {
            TouristsNumber = touristsNumber;
            Attendance = attendance;
            Notifications = new List<TouristNotifications>();

        }
    }
}
