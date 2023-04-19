using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class ReservationReschedulingRequestRepository
    {
        public ReservationReschedulingRequestRepository() { }

        public List<ReservationReschedulingRequest> GetAll()
        {

            List<ReservationReschedulingRequest> reservationReschedulingRequests = new();

            using (DataContext db = new())
            {
                reservationReschedulingRequests = db.ReservationReschedulingRequests.
                    Include(t => t.Reservation)
                        .ThenInclude(t => t.Accomodations)
                    .Include(t => t.Reservation)
                        .ThenInclude(t => t.User).ToList();
            }

            return reservationReschedulingRequests;

        }

        public void UpdateCommentBy(int id, string comment)
        {
            ReservationReschedulingRequest reservationReschedulingRequest = new();

            var db = new DataContext();
            reservationReschedulingRequest = db.ReservationReschedulingRequests.Find(id);

            reservationReschedulingRequest.Comment = comment;
            db.SaveChanges();
        }

        public void UpdateStateBy(int id, RequestState requestState)
        {
            ReservationReschedulingRequest reservationReschedulingRequest = new();

            var db = new DataContext();
            reservationReschedulingRequest = db.ReservationReschedulingRequests.Find(id);

            reservationReschedulingRequest.State = requestState;
            db.SaveChanges();


        }

        public ReservationReschedulingRequest GetById(int Id)
        {
            ReservationReschedulingRequest reservationReschedulingRequest = new();

            using (DataContext db = new())
            {
                reservationReschedulingRequest = (ReservationReschedulingRequest)db.ReservationReschedulingRequests
                                                .Include(t => t.Reservation)
                                                .Where(t => t.Id.Equals(Id)).First();
            }
            return reservationReschedulingRequest;

        }
    }
}
