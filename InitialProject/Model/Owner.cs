using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Owner : User
    {

        public bool SuperOwner { get; set; }
        public List<GuestRating> GuestRatings { get; set; }

        public List<Accomodation> Accomodations { get; set; }

        public Owner(string username, string password, UserType userType) : base (username, password, userType) 
        { }
    }
}
