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
    /// Interaction logic for GuestStat.xaml
    /// </summary>
    public partial class GuestStat : Window
    {
        public GuestStat()
        {
            InitializeComponent();
        }

        private void ShowTour_Click(object sender, RoutedEventArgs e)
        {
            K2_F2_Guide k2_F2_Guide = new K2_F2_Guide();


            List<TourDateDTO> tourDateDTO = k2_F2_Guide.FinishedTours();

            ListOfTours.ItemsSource = tourDateDTO;
        }

        private void ShowStat_Click(object sender, RoutedEventArgs e)
        {
            int dateID = Int32.Parse(DateId.Text);
            int tourId = Int32.Parse(TourId.Text);

            K2_F2_Guide k2_F2_Guide = new K2_F2_Guide();

            GuestAgeStatisticDTO guestAgeStatisticDTO = new GuestAgeStatisticDTO();
            List<GuestAgeStatisticDTO> list = new List<GuestAgeStatisticDTO>();
            DatesRepository datesRepository = new DatesRepository();
            Dates date = datesRepository.GetById(dateID);

            guestAgeStatisticDTO = k2_F2_Guide.GuestAgeStatisticDTO(date, tourId);
            list.Add(guestAgeStatisticDTO);

            ListOfStat.ItemsSource= list;

        }

        private void DateId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TourId1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
