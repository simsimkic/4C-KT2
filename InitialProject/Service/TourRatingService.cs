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
    public class TourRatingService
    {
        TourRatingRepository tourRatingRepository = new TourRatingRepository();

        public TourRatingService() { }

        public void Add(TourRating tourRating, int touristId, int tourId)
        {
            tourRatingRepository.Add(tourRating, touristId, tourId);
        }

        public List<TourRating> GetAll()
        {
            return tourRatingRepository.GetAll();
        }

        public TourRating GetById(int id)
        {
            return tourRatingRepository.GetById(id);

        }


    }
}
