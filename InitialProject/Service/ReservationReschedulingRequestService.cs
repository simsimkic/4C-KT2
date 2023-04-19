using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class ReservationReschedulingRequestService
    {

        ReservationReschedulingRequestRepository reservationReschedulingRequestRepository = new ReservationReschedulingRequestRepository();
        AccomodationReservationService accomodationReservationService = new AccomodationReservationService();
        public ReservationReschedulingRequestService() { }   
 
        public List<ReservationReschedulingRequest> GetAll ()
        {
            return reservationReschedulingRequestRepository.GetAll();
        }

        public void DeclareRespond (string comment, RequestState requestState, int id)
        {
            this.reservationReschedulingRequestRepository.UpdateCommentBy(id, comment); 
            this.reservationReschedulingRequestRepository.UpdateStateBy(id, requestState);
            this.UpdateReservationDates(id);
        }

        private void UpdateReservationDates(int requestId)
        {
            ReservationReschedulingRequest existingRequest = new ReservationReschedulingRequest();
            int reservationId = existingRequest.Reservation.AccomodationReservationId;

            if (existingRequest.State == RequestState.Approved) 
            {
                this.accomodationReservationService.UpdateScheduledDatesBy(reservationId, existingRequest.NewStartingDate, existingRequest.NewEndingDate);
            }
        }

        public bool CanBePostponed(ReservationReschedulingRequest request)
        {
            bool postponementFeasibility; 

            List<AccomodationReservation> existingReservations = new List<AccomodationReservation> ();

            existingReservations = accomodationReservationService.GetAllNotCancelled(request); 

            if (existingReservations.Count() == 0)
            {
                postponementFeasibility = true;
            } else
            {
                postponementFeasibility = false;
            }

            return postponementFeasibility;
        }


    }
}
