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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for MostVisited.xaml
    /// </summary>
    public partial class MostVisited : Window
    {
        
        public MostVisited()
        {
            InitializeComponent();
            
        }

        private void ShowTour_Click(object sender, RoutedEventArgs e)
        {
            K2_F2_Guide k2_F2_Guide = new K2_F2_Guide();
            DatesRepository datesRepository = new DatesRepository();

            List<Dates> dates = datesRepository.GetAll();

            List<TourDateDTO> dto = new List<TourDateDTO>();
            TourDateDTO tourDateDTO = new TourDateDTO();
            tourDateDTO = k2_F2_Guide.ShowMostVisitedTour(dates);
            if (tourDateDTO != null)
            {
                dto.Add(tourDateDTO);
                ListOfTours.ItemsSource = dto;
            } else
            {
                FreePlacesLabel.Content = "nijedna tura nema prijavljenog turistu";
            }
        }

        private void TourDates_Click(object sender, RoutedEventArgs e)
        {
            K2_F2_Guide k2_F2_Guide = new K2_F2_Guide();
            DatesRepository datesRepository = new DatesRepository();


            List<TourDateDTO> dto = new List<TourDateDTO>();
            TourDateDTO tourDateDTO = new TourDateDTO();
            
            int year = Int32.Parse(TourNameCancel.Text);
            List<Dates> dates = datesRepository.GetByYear(year);
            tourDateDTO = k2_F2_Guide.ShowMostVisitedTour(dates);
            if (tourDateDTO != null)
            {
                dto.Add(tourDateDTO);
                ListOfTours.ItemsSource = dto;

            }
            else
            {
                FreePlacesLabel.Content = "nijedna tura nema prijavljenog turistu za tu godinu";
            }
        }
    }
}
