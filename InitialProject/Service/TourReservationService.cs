using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace InitialProject.Service
{
    public class TourReservationService
    {
        TourReservationRepository tourReservationRepository = new TourReservationRepository();
        public TourReservationService()
        {
        }

        public List<TourReservation> GetByTour(Tour tour)
        {
            return tourReservationRepository.GetByTour(tour);
        }

        public List<TourReservation> GetByTourist(int touristId)
        {
            return tourReservationRepository.GetByTourist(touristId);
        }


        public TourReservation GetByNotification(int notificationId)
        {
            return tourReservationRepository.GetByNotification(notificationId);
        }

        public void UpdateAttendance(int tourReservationId)
        {
            tourReservationRepository.UpdateAttendance(tourReservationId);
        }


    }
}
