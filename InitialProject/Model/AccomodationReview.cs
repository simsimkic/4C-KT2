using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class AccomodationReview
    {
        [Key]
        public int AccomodationId { get; set; }


        public int Rating { get; set; }

        public string Recommendations { get; set; }


        public AccomodationReview() { }

        public AccomodationReview(int accomodationId, int rating, string recommendations)
        {
            AccomodationId = accomodationId;
            Rating = rating;
            Recommendations = recommendations;
        }

        public override string ToString()
        {
            return $"AccomodationId: {AccomodationId}\n, Rating: {Rating}\n, Recommendation: {Recommendations}\n";
        }
    }
}
