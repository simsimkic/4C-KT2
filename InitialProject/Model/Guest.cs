using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Guest : User
    {
        public bool IsPresent { get; set; }
        
        public List<AccomodationReservation> AccomodationReservations { get; set; }

        public List<Accomodation> Accomodations { get; set; }

        public List<Comment> Comments { get; set; }

        public List<AccomodationReservation> AccomodationReservations { get; set; }

        public Guest(string username, string password, UserType userType) : base(username, password, userType) 
        {

        }


    }
}
