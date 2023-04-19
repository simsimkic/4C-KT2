using InitialProject.DTO;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for Review.xaml
    /// </summary>
    public partial class Review : Window
    {   
        public int DateId { get; set; }
        public Review(int dateId)
        {
            InitializeComponent();
            DateId = dateId;
        }

        private void ShowTour_Click(object sender, RoutedEventArgs e)
        {
            K2_F3_Guide k2_F3_Guide = new K2_F3_Guide();

            List<TourRatingCheckpointDTO> list = k2_F3_Guide.ShowReview(DateId);
            ListOfTours.ItemsSource = list;
        }
    }
}
