using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace InitialProject.Repository
{
    public class AccomodationRatingRepository
    {
        public AccomodationRatingRepository() { }
        

        public List<AccomodationRating> GetAllAccomodationRatings()
        {
            using (var db = new DataContext())
            {
                return db.AccomodationRating.ToList();
            }
        }

        public void AddAccomodationRating(int accId,int ownerId,int cleanliness,int ownerFriendliness,int commentId,string commentText,int imageId,string imageName,string imageURL, DateTime timeGone)
        {
            using (var db = new DataContext())
            {
                if (DateTime.Now.Date <= timeGone.AddDays(5))
                {
                    AccomodationImage accomodationImage = new AccomodationImage(imageId, imageName, imageURL);
                    Comment comment = new Comment(commentId, DateTime.Now, commentText);

                    List<Comment> commentList = new List<Comment>();
                    commentList.Add(comment);
                    List<AccomodationImage> accomodationImageList = new List<AccomodationImage>();
                    accomodationImageList.Add(accomodationImage);

                    AccomodationRating rating = new AccomodationRating(accId, ownerId, cleanliness, ownerFriendliness, commentList, accomodationImageList);
                    db.AccomodationRating.Add(rating);
                    Console.WriteLine("Succesfully added rating!");

                    db.AccomodationImages.Add(accomodationImage);
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return;
                }
                else
                {
                    Console.WriteLine("It's been 5 days and you can't rate the accommodation.");
                    return;
                }
            }
        }

        

    }
}
