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
    /// Interaction logic for TourDates.xaml
    /// </summary>
    public partial class TourDates : Window
    {   
        public Tour Tour { get; set; }
        public TourDates()
        {
            InitializeComponent();
        }

        public TourDates(Tour tour)
        {
            InitializeComponent();
            Tour = tour;
        }

        private void ShowTour_Click(object sender, RoutedEventArgs e)
        {
            
                using (var db = new DataContext())
                {
                    DatesRepository datesRepository = new DatesRepository();
                    TourRepository tourRepository = new TourRepository();

                    List<Dates> dates = new List<Dates>();
                       
                    foreach(var date in Tour.StartingDates) 
                    {
                        if (DateTime.Compare(date.Date, DateTime.Now) > 0)
                        {

                            dates.Add(date);
                        }
                    }



                    ListOfTours.ItemsSource = dates;

                }
            
        }

        private void CancelDate_Click(object sender, RoutedEventArgs e)
        {
            TourRepository tourRepository = new TourRepository();
            TourService tourService = new TourService();
            KT2_F1_Guide kT2_F1_Guide = new KT2_F1_Guide(); 
            var tourId = int.Parse(TourIdCancel.Text);
            string content = "";
            
            
            Tour tour = tourRepository.GetById(Tour.TourId);

            if (Tour.GuideId != UserSession.LoggedInUser.Id)
            {
                content = "Niste kreirali ovu turu";
                
                FreePlacesLabel.Content = content;
                CancelTour cancelTour = new CancelTour();
                
            }
            else
            {

                bool CancelTourInfo = kT2_F1_Guide.CancelTour(tourId);

                if (CancelTourInfo == false)
                {
                    content = "Ne mozete otkazati turu koja krece za manje od 48h!";
                }
                else
                {
                    content = "Uspesno otkazana tura!";
                }

                FreePlacesLabel.Content = content;
            }
            }
    }
    
}
