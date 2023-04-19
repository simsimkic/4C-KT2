using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class OwnerReview
    {
        public int Id { get; set; }

        public int Cleanliness { get; set; }

        public int OwnerFairness { get; set; }

        public string Comment { get; set; }

        public AccomodationReservation AccomodationReservation { get; set; }

        public string Images { get; set; }

        public OwnerReview() { }

        public OwnerReview(int cleanliness, int ownerFairness, String images)
        {
           
            Cleanliness = cleanliness;
            OwnerFairness = ownerFairness;
            Images = images;

        }
    }
}
