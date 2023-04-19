using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace InitialProject.Repository
{
    public class TourReservationRepository
    {
        public TourReservationRepository()
        {

        }
        public List<TourReservation> GetAllTourReservations()
        {
            using (var db = new DataContext())
            {
                return db.TourReservations.ToList();
            }
        }

        public List<TourReservation> GetByTour(Tour tour)
        {
            using (var db = new DataContext())
            {
                var tourToReturn = db.Tours.Include(t => t.TourReservations).FirstOrDefault(t => t.TourId == tour.TourId);
                if (tourToReturn == null) {
                    return null;
                }
                List<TourReservation> tourReservations = new List<TourReservation>();
                tourReservations.AddRange(tourToReturn.TourReservations);
                return tourReservations;
            }
        }

        public List<TourReservation> GetByTourist(int touristId)
        {
            using (var db = new DataContext())
            {
                var touristToReturn = db.Tourists.Include(t => t.TourReservations).FirstOrDefault(t => t.Id == touristId);
                if (touristToReturn == null)
                {
                    return null;
                }
                List<TourReservation> tourReservations = new List<TourReservation>();
                tourReservations.AddRange(touristToReturn.TourReservations);
                return tourReservations;
            }
        }

        public TourReservation GetByNotification(int notificationId)
        {
            using (var db = new DataContext())
            {
                List<TourReservation> tourReservations = db.TourReservations.Include(t => t.Notifications).ToList();

                foreach (TourReservation tourReservation in tourReservations)
                {
                    foreach (TouristNotifications touristNotification in tourReservation.Notifications)
                    {
                        if (touristNotification.Id == notificationId)
                        {
                            return tourReservation;
                        }
                    }
                }
                return null;
            }
        }

        public void UpdateAttendance(int tourReservationId)
        {
            using (var db = new DataContext())
            {
                var tourReservation = db.TourReservations.FirstOrDefault(t => t.TourReservationId == tourReservationId);
                tourReservation.Attendance = true;
                db.TourReservations.Update(tourReservation);
                db.SaveChanges();
            }
        }
    }
}
