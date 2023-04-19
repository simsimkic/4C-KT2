using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InitialProject.DTO
{
    public class OwnerReviewDto
    {

        public int ReservationId { get; set; } 

        public int Cleanliness { get; set; } 

        public int OwnerFairness { get; set; } 

        public string Comment { get; set; } 

        public string Images { get; set; }

        public OwnerReviewDto(OwnerReview ownerReview) 
        {
            this.ReservationId = ownerReview.AccomodationReservation.AccomodationReservationId;
            this.Cleanliness = ownerReview.Cleanliness;
            this.OwnerFairness = ownerReview.OwnerFairness;
            this.Comment = ownerReview.Comment;
            this.Images = ownerReview.Images;
        }
    }
}
