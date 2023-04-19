using InitialProject.Commands;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.ViewModel
{
    public class TouristViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ShowCouponsCommand { get; set; }

        public ICommand BookCommand { get; set; }

        public ICommand RateTourCommand { get; set; }

        public ICommand TrackTourCommand { get; set; }

        public ICommand ShowNotificationsCommand { get; set; }

        public TouristViewModel()
        {
            ShowCouponsCommand = new DelegateCommand(ShowCoupons);
            BookCommand = new DelegateCommand(BookTour);
            RateTourCommand = new DelegateCommand(RateTour);
            TrackTourCommand = new DelegateCommand(TrackTour);
            ShowNotificationsCommand = new DelegateCommand(ShowNotifications);
        }

        private void ShowCoupons()
        {
            var touristCouponsWindow = new TouristCouponsWindow();
            touristCouponsWindow.ShowDialog();
        }

        private void BookTour()
        {
            var tourReservationWindow = new TourReservationWindow();
            tourReservationWindow.ShowDialog();
        }

        private void RateTour()
        {
            var tourRatingWindoww = new TourRatingWindow();
            tourRatingWindoww.ShowDialog();
        }

        private void TrackTour()
        {
            var trackTourWindow = new TrackTourWindow();
            trackTourWindow.ShowDialog();
        }

        private void ShowNotifications()
        {
            var touristNotificationsWindow = new TouristNotificationsWindow();
            touristNotificationsWindow.ShowDialog();
        }
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action CloseAction { get; set; }

    }
}
