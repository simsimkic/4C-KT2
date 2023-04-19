using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using System;
using WebApi.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;



namespace InitialProject.Service
{
    public class K2_F3_Guide
    {
        TourRepository tourRepository = new TourRepository();
        DatesRepository datesRepository = new DatesRepository();
        TouristsRepository touristsRepository = new TouristsRepository();
        CheckpointRepository checkpointRepository = new CheckpointRepository();
        public List<TourRatingCheckpointDTO> ShowReview(int dateId)
        {
            
            DatesRepository datesRepository = new DatesRepository();

            Dates date = datesRepository.GetByIdRatings(dateId);
            List<Checkpoint> checkpoints = checkpointRepository.GetAllCheckpoints();

            List<TourRating> ratings = new List<TourRating>();
            List<TourRatingCheckpointDTO> toursToReturn = new List<TourRatingCheckpointDTO>();

            if (date == null) return null;

            Ratings(date, ref ratings);

            MakeRatingsToReturn(checkpoints, ratings, ref toursToReturn);
            
            
            return toursToReturn;
        }

        public void Ratings(Dates date, ref List<TourRating> ratings)
        {
            foreach (var tourist in date.Tourists)
            {
                IterateToMakeRating(tourist, ref ratings);
            }
        }

        public void IterateToMakeRating(Tourist tourist, ref List<TourRating> ratings)
        {
            foreach (var rating in tourist.TourRatings)
            {
                MakeRating(tourist, rating, ref ratings);
            }
        }

        public void MakeRating(Tourist tourist, TourRating rating, ref List<TourRating> ratings)
        {
            TourRating tourRating = new TourRating(tourist.Id, rating.GuideKnowledge, rating.GuideLanguage, rating.TourAmusement, rating.Comment);
            ratings.Add(tourRating);
        }

        public void MakeRatingsToReturn(List<Checkpoint> checkpoints, List<TourRating> ratings, ref List<TourRatingCheckpointDTO> toursToReturn)
        {

            foreach (var checkpoint in checkpoints)
            {
                IterateTourists(checkpoint.Tourists, ref toursToReturn, checkpoint, ratings);
                
            }

            
        }

        public void IterateTourists(List<Tourist> tourists, ref List<TourRatingCheckpointDTO> toursToReturn, Checkpoint checkpoint, List<TourRating> ratings)
        {
            foreach (var tourist in checkpoint.Tourists)
            {
                IterateRatings(ref toursToReturn, ratings, tourist, checkpoint);
            }
            
        }

        public void IterateRatings(ref List<TourRatingCheckpointDTO> toursToReturn, List<TourRating> ratings, Tourist tourist, Checkpoint checkpoint)
        {
            foreach (var tourRating in ratings)
            {
                if (tourRating.TouristId == tourist.Id)
                {
                    MakeTourRating(tourRating, checkpoint, ref toursToReturn);
                }
            }
        }

        public void MakeTourRating(TourRating tourRating, Checkpoint checkpoint, ref List<TourRatingCheckpointDTO> toursToReturn)
        {
            TourRatingCheckpointDTO tourRatingCheckpointDTO = new TourRatingCheckpointDTO(tourRating.GuideKnowledge, tourRating.GuideLanguage, tourRating.TourAmusement, tourRating.Comment, checkpoint.Name);
            toursToReturn.Add(tourRatingCheckpointDTO);
        }
    }
}
