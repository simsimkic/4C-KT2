using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace InitialProject.Model
{
    public class TourRating
    {
        [Key]
        public int Id { get; set; }

        public int TouristId { get; set; }
        public int GuideKnowledge { get; set; } = 0;

        public int GuideLanguage { get; set; } = 0;
        
        public int TourAmusement { get; set; } = 0;

        public string Comment { get; set; } = "";

        //public bool IsValid { get; set; }
        public List<TourImages> TourImages { get; set; }

        public TourRating()
        {
            TourImages = new List<TourImages>();
        }
        public TourRating(int guideKnowledge, int guideLanguage, int tourAmusement, string comment, List<TourImages> tourImages)
        {   
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourAmusement = tourAmusement;
            Comment = comment;
            TourImages = tourImages;
            
        }
        public TourRating(int touristId,int guideKnowledge, int guideLanguage, int tourAmusement, string comment, List<TourImages> tourImages)
        {   
            TouristId = touristId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourAmusement = tourAmusement;
            Comment = comment;
            TourImages = tourImages;
            
        }
        public TourRating(int touristId, int guideKnowledge, int guideLanguage, int tourAmusement, string comment)
        {
            TouristId = touristId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourAmusement = tourAmusement;
            Comment = comment;
            TourImages = new List<TourImages>();

        }



    }
}
