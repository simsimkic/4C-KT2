using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class ReservationReschedulingRequest
    {
        public int Id { get; set; } 

        public RequestState State { get; set; } 

        public string Comment { get; set; } 

        public AccomodationReservation Reservation { get; set; }

        public DateTime NewStartingDate { get; set; } 

        public DateTime NewEndingDate { get; set; } 

        public bool Achievable { get; set; }

        public ReservationReschedulingRequest() { } 

        public ReservationReschedulingRequest (RequestState state, string comment, AccomodationReservation reservation)
        {
            State = state;
            Comment = comment;
            Reservation = reservation;

        }
    }
}
