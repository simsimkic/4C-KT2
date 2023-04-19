using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for ReviewsWindow.xaml
    /// </summary>
    public partial class ReviewsWindow : Window
    {

        private readonly OwnerReviewService OwnerReviewService = new(new OwnerReviewRepository()); 


        private List<OwnerReviewDto> visibleReviews = new List<OwnerReviewDto>();
        private List<OwnerReview> ownerReviews = new List<OwnerReview>();

        public ReviewsWindow()
        {
            InitializeComponent();
            InitializeReviewReports();

            this.ownerReviews = OwnerReviewService.GetAllOwnerReviews();
        }

        public List<OwnerReviewDto> GetAllGraded()
        {
            List<OwnerReviewDto> visibleReviews = new List<OwnerReviewDto>(); 

            foreach(var review in OwnerReviewService.GetAllGraded())
            {
                visibleReviews.Add(new OwnerReviewDto(review));
            }
            return visibleReviews;
        }

        public void InitializeReviewReports()
        {
            this.visibleReviews = GetAllGraded();
            reviews.ItemsSource = visibleReviews;
        }

        public double SumGrades()
        {

            double gradeSum = 0;

            foreach (var review in ownerReviews)
            {
                gradeSum += (review.OwnerFairness + review.Cleanliness);
            }
            return gradeSum;
        }

        public double CountGrades()
        {
            double gradeCount = 0;

            foreach (var review in ownerReviews)
            {
                gradeCount++;
            }
            return gradeCount;
        }

        public void DeclareTitle()
        {

            double average = Math.Round(SumGrades() / CountGrades(), 2);

            if (average >= 9.5 && SumGrades() >= 50)
            {
                TitlePlaceHolder.Text = "Super-Owner";
                OwnerReviewService.DeclareOwner(true);
            }
            else
            {
                TitlePlaceHolder.Text = "Owner";
                OwnerReviewService.DeclareOwner(false);
            }
        }


    }
}
