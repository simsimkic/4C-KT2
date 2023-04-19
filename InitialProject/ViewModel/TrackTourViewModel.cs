using InitialProject.Commands;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WebApi.Entities;

namespace InitialProject.ViewModel
{
    public class TrackTourViewModel : INotifyPropertyChanged
    {

        private string _tourName;

        public ICommand ConfirmCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        TourService tourService = new TourService();

        public TrackTourViewModel()
        {
            ConfirmCommand = new DelegateCommand(Confirm, CanConfirm);

        }
        public string TourName
        {
            get { return _tourName; }
            set { _tourName = value; RaisePropertyChanged(nameof(TourName)); }
        }

        public bool CanConfirm()
        {
            return !string.IsNullOrEmpty(TourName);
        }

        public bool IsTourActive(Tour tour)
        {
            return tourService.IsTourActive(tour);

        }

        private void Confirm()
        {
            Tour tour = tourService.GetByName(TourName);
            if (tour == null)
            {
                MessageBox.Show("Tour doesn't exist.");
                return;
            }
            if (IsTourActive(tour) == false)
            {
                MessageBox.Show("Tour isn't active.");
                return;
            }
            if (CanTouristTrack())
            {
                TourAttendanceWindow tourAttendanceWindow = new TourAttendanceWindow();
                TourAttendanceViewModel tourAttendanceViewModel = (TourAttendanceViewModel)tourAttendanceWindow.DataContext;
                tourAttendanceViewModel.TourName = TourName;
                tourAttendanceWindow.Show();
            }
            else
            {
                MessageBox.Show("You can't track this tour");
            }
        }

        public bool CanTouristTrack()
        {
            Tour tour = tourService.GetByName(TourName);
            if (tour == null)
            {
                MessageBox.Show("No such tour!");
            }

            TouristsService touristsService = new TouristsService();
            return touristsService.CanTouristTrack(UserSession.LoggedInUser.Id, tour);

        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action CloseAction { get; set; }
    }
}
