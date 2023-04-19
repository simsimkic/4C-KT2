using InitialProject.Interface;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class OwnerReviewRepository : IOwnerReviewRepository
    {

        public OwnerReviewRepository() { }
        public List<OwnerReview> GetAllOwnerReviews()
        {
            using (var db = new DataContext())
            {
                return db.OwnerReviews.Include(t => t.AccomodationReservation)
                                        .ThenInclude(t => t.User)
                                      .Include(t => t.AccomodationReservation)
                                        .ThenInclude(t => t.Accomodations).ToList();
            }
        }
    }
}
