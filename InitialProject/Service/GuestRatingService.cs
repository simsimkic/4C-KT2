using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class GuestRatingService
    {
        public GuestRatingService() { } 

        GuestRatingRepository guestRatingRepository = new GuestRatingRepository();
        AccomodationReservationService accomodationReservationService = new AccomodationReservationService(); 
        

        
        public List<AccomodationReservation> GetGradedReservations() //clean code: GetGraded?
        {
            
            List<AccomodationReservation> accomodationReservations = new List<AccomodationReservation>(); 
            List<GuestRating> guestRatings = new List<GuestRating>();

            guestRatings = guestRatingRepository.GetAllGuestRatings();

            foreach (var ratings in guestRatings)
            {
                accomodationReservations.Add(ratings.AccomodationReservation);
            }
            return accomodationReservations;
        } 

        public List<AccomodationReservation> GetNotGradedExpiredReservations() //clean code: GetNotGradedExpired?
        {

            List<AccomodationReservation> expiredReservations = accomodationReservationService.GetAllExpired();

            List<AccomodationReservation> gradedReservations = GetGradedReservations();


            List<AccomodationReservation> nonGradedExpired = new List<AccomodationReservation>();

            foreach (AccomodationReservation r in expiredReservations)
            {
                bool exists = false;
                foreach (AccomodationReservation g in gradedReservations)
                {

                    if (r.AccomodationReservationId == g.AccomodationReservationId)
                    {
                        exists = true;
                    }
                }

                if (!exists)
                {
                    nonGradedExpired.Add(r);
                }
            }
            return nonGradedExpired;
        } 
    } 
}
