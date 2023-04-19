using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace InitialProject.Service
{
    public class AccomodationReservationService
    {
        public AccomodationReservationService() { }

        private int accomodationId;
        private int reservedAccomodationId;

        AccomodationReservationRepository accomodationReservationRepository = new AccomodationReservationRepository();

        DateTime todaysDate = DateTime.UtcNow.Date;

        public List<AccomodationReservation> GetAllExpired()
        {
            return accomodationReservationRepository.GetAllExpiredBy(todaysDate.Day, todaysDate.Month, todaysDate.Year);
        }

        /*AccomodationReservationRepository accomodationReservationRepository = new AccomodationReservationRepository();
        DateTime todaysDate = DateTime.UtcNow.Date;

        public List<AccomodationReservation> GetAllExpired()
        {
           return  accomodationReservationRepository.GetAllExpiredBy(todaysDate.Day, todaysDate.Month, todaysDate.Year); 
        } */

        public List<AccomodationReservation> GetAllNotCancelled(ReservationReschedulingRequest reschedulingRequest)
        {
            int reservedAccomodationId;
            List<AccomodationReservation> preservedReservations = new List<AccomodationReservation>();
            List<AccomodationReservation> reservations = new List<AccomodationReservation>();

            int accomodationId = GetAccomodationId(reschedulingRequest);
            int reservationId = reschedulingRequest.Reservation.AccomodationReservationId;

            reservations = this.accomodationReservationRepository.GetAllBetween(reschedulingRequest.NewStartingDate, reschedulingRequest.NewEndingDate);

            foreach (var reservation in reservations)
            {
                reservedAccomodationId = GetReservationAccomodationId(reservation); 

                if (!reservation.Cancelled && reservedAccomodationId == accomodationId && reservationId != reservation.AccomodationReservationId) 
                {
                    preservedReservations.Add(reservation);
                }

            }
            return preservedReservations;

        }

        public void UpdateScheduledDatesBy(int id, DateTime newBegginingDate, DateTime newEndingDate)
        {
            this.accomodationReservationRepository.UpdateScheduledDatesBy(id, newBegginingDate, newEndingDate);
        }

        public int GetAccomodationId(ReservationReschedulingRequest reservationReschedulingRequest)
        {

            foreach (var accomodations in reservationReschedulingRequest.Reservation.Accomodations)
            {
                accomodationId = accomodations.AccId;
                break;
            }
            return accomodationId;
        }

        public int GetReservationAccomodationId(AccomodationReservation accomodationReservation)
        {

            foreach (var accomodations in accomodationReservation.Accomodations)
            {
                accomodationId = accomodations.AccId;
                break;
            }
            return accomodationId;
        }
    }
}
