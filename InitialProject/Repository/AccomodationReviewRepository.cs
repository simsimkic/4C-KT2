using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class AccomodationReviewRepository
    {
        public AccomodationReviewRepository() { }

        public List<AccomodationReview> GetAllAccomodationReview() 
        {
            using (var db = new DataContext())
            {
                return db.AccomodationReviews.ToList();
            }
        }
        public void AddAccomodationReview(int accId,int rating,string recommodation)
        {
            using (var db = new DataContext())
            {
                AccomodationReview accomodationReview = new AccomodationReview(accId,rating, recommodation);
                db.AccomodationReviews.Add(accomodationReview);
                Console.WriteLine("Successfully added recommendation for renovation!");
                db.SaveChanges();
            }
        }


    }
}
