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
    public class TouristsService
    {
        TouristsRepository touristsRepository = new TouristsRepository();
        public Tourist GetById(int id)
        {
            return touristsRepository.GetById(id);
        }
        public List<Tourist> GetTourists(int tourId)
        {
            return touristsRepository.GetTourists(tourId);

        }

        public List<Coupon> GetTouristCoupons(int touristId)
        {
            return touristsRepository.GetTouristCoupons(touristId);

        }
        public List<Coupon> GetUsableTouristCoupons(int touristId)
        {
            return touristsRepository.GetUsableTouristCoupons(touristId);

        }

        public bool CanTouristTrack(int touristId, Tour tour)
        {
            TourReservationService tourReservationService = new TourReservationService();
            List<TourReservation> touristReservations = tourReservationService.GetByTourist(touristId);
            List<TourReservation> tourReservations = tourReservationService.GetByTour(tour);

            foreach (TourReservation tourReservation in tourReservations)
            {
                foreach (TourReservation touristReservation in touristReservations)
                {
                    if (tourReservation.TourReservationId == touristReservation.TourReservationId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanTouristRate(int touristId, Tour tour)
        {
            TourReservationService tourReservationService = new TourReservationService();
            List<TourReservation> touristReservations = tourReservationService.GetByTourist(touristId);
            List<TourReservation> tourReservations = tourReservationService.GetByTour(tour);

            foreach (TourReservation tourReservation in tourReservations)
            {
                foreach (TourReservation touristReservation in touristReservations)
                {
                    if (tourReservation.TourReservationId == touristReservation.TourReservationId)
                    {
                        if (touristReservation.Attendance)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


    }
}
