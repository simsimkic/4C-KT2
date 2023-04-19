using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WebApi.Entities;

namespace InitialProject.ViewModel
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";

            TourReservationService tourReservationService = new TourReservationService();
            TourReservation tourReservation = tourReservationService.GetByNotification((int) value);
            TourService tourService = new TourService();
            Tour tour = tourService.GetByTourReservation(tourReservation.TourReservationId);

            return tour.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }

}
