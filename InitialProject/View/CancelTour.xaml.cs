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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApi.Entities;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CancelTour.xaml
    /// </summary>
    public partial class CancelTour : Window
    {
        public CancelTour()
        {
            InitializeComponent();

        }

        //public void CancelTour_Click(object sender, RoutedEventArgs e) { }
        public void ShowTour_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DataContext())
            {
                TourRepository tourRepository = new TourRepository();
                List<Tour> tours = tourRepository.GetAll();

                ListOfTours.ItemsSource = tours;

            }
        }

        public void TourDates_Click(object sender, RoutedEventArgs e)
        {   
            using (var db = new DataContext()) 
            {
                TourRepository tourRepository = new TourRepository();

                string tourName = TourNameCancel.Text;
                
                Tour tour = tourRepository.GetByName(tourName);

                TourDates tourDates = new TourDates(tour);

                tourDates.Tour = tour;
                if (tourDates.Tour.GuideId == UserSession.LoggedInUser.Id)
                {
                    if (tourDates.Tour != null) { tourDates.Show(); }
                }
                else
                {
                    FreePlacesLabel.Content = "Ne mozete otkazati turu koju niste kreirali";
                }
                //Close();
            }
        }

    }
}
