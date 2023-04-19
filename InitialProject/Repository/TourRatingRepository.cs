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
    public class TourRatingRepository
    {
        public TourRatingRepository() { }

        public void Add(TourRating tourRating, int touristId, int tourId)
        {
            using (var db = new DataContext())
            {
                TouristsRepository touristRepository = new TouristsRepository();
                Tourist loggedInTourist = db.Tourists.Find(touristId);
                Tour ratedTour = db.Tours.Find(tourId);
                db.TourRatings.Add(tourRating);
                loggedInTourist.TourRatings.Add(tourRating);
                ratedTour.TourRatings.Add(tourRating);
                ratedTour.Images.AddRange(tourRating.TourImages);
                db.SaveChanges();
            }
        }

        public List<TourRating> GetAll()
        {
            using (var db = new DataContext())
            {

                return db.TourRatings.ToList();
            }
        }

        public TourRating GetById(int id)
        {
            using (var db = new DataContext())
            {
                return db.TourRatings.FirstOrDefault(t => t.Id == id);
            }
        }


    }
}
