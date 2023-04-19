using InitialProject.Commands;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WebApi.Entities;

namespace InitialProject.ViewModel
{
    public class TouristNotificationsViewModel : INotifyPropertyChanged
    {

        private string _tourName;

        private ObservableCollection<TouristNotifications> _touristNotifications;

        TourNotificationsService tourNotificationsService = new TourNotificationsService();
        public ICommand ConfirmCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TouristNotificationsViewModel()
        {
            using (var db = new DataContext())
            {
                TouristNotifications = new ObservableCollection<TouristNotifications>(tourNotificationsService.GetByTourist(UserSession.LoggedInUser.Id));
                ConfirmCommand = new DelegateCommand(Confirm);
            }
        }       

        public string TourName
        {
            get { return _tourName; }
            set { _tourName = value; RaisePropertyChanged(nameof(TourName)); }
        }


        public ObservableCollection<TouristNotifications> TouristNotifications
        {
            get { return _touristNotifications; }
            set
            {
                _touristNotifications = value;
                RaisePropertyChanged(nameof(TouristNotifications));
            }
        }


        public void Confirm(object parameter)
        {
            TouristNotifications touristNotification = (TouristNotifications)parameter;
            TourReservationService tourReservationService = new TourReservationService();
            TourService tourService = new TourService();

            TourReservation tourReservation = tourReservationService.GetByNotification(touristNotification.Id);
            tourReservationService.UpdateAttendance(tourReservation.TourReservationId);
            Tour tour = tourService.GetByTourReservation(tourReservation.TourReservationId);
            tourNotificationsService.Delete(touristNotification);
            TouristNotifications = new ObservableCollection<TouristNotifications>(tourNotificationsService.GetByTourist(UserSession.LoggedInUser.Id));
        }
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action CloseAction { get; set; }
    }
}
