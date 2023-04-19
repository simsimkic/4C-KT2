
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace InitialProject.Model
{
    public class AccomodationReservation
    {   
        public int AccomodationReservationId { get; set; }
        public DateTime CheckInDate { get; set; } 
        public DateTime CheckOutDate { get; set; } 

        public int NumberOfGuests { get; set; } 

        public List<Accomodation> Accomodations { get; set; } 
        public User User { get; set; } 

        public bool Cancelled { get; set; }

        public override string ToString()
        {
            return "CheckIn " + CheckInDate + " CheckOut " + CheckOutDate + " NumberofGuests " + NumberOfGuests;
        }

        public AccomodationReservation() 
        { 
            
            Accomodations = new List<Accomodation>();
        }

        public AccomodationReservation(int id, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests) 
        {
            AccomodationReservationId = id; 
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            NumberOfGuests = numberOfGuests;
            
            Accomodations = new List<Accomodation>();

        }

        public AccomodationReservation(DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
        {
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            NumberOfGuests = numberOfGuests;
            
            Accomodations = new List<Accomodation>();
        }

    }
}

