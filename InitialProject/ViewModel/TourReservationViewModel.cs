using InitialProject.Commands;
using InitialProject.Model;
using InitialProject.Service;
using InitialProject.View;
using Microsoft.Win32;
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
    public class TourReservationViewModel
    {
        private string _tourName;
        private int _tourists;
        private bool _isChangeEnabled;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BookCommand { get; set; }
        public ICommand ChangeTouristsNumberCommand { get; set; }
        public ICommand ChooseTourCommand { get; set; }
        public ICommand CancelReservationCommand { get; set; }
        public ICommand NavigateToConfirmationWindowCommand { get; set; }

        public TourReservationViewModel()
        {
            BookCommand = new DelegateCommand(Book, CanBook);
            ChangeTouristsNumberCommand = new DelegateCommand(ChangeTouristsNumber, CanChange);
            ChooseTourCommand = new DelegateCommand(ChooseTour, CanChange);
            CancelReservationCommand = new DelegateCommand(CancelReservation, CanCancel);
            NavigateToConfirmationWindowCommand = new DelegateCommand(NavigateToConfirmationWindow);
        }

        public string TourName
        {
            get { return _tourName; }
            set { _tourName = value; RaisePropertyChanged(nameof(TourName)); }
        }

        public int Tourists
        {
            get { return _tourists; }
            set
            {
                _tourists = value;
                RaisePropertyChanged(nameof(Tourists));
            }
        }

        public bool IsChangeEnabled
        {
            get { return _isChangeEnabled; }
            set
            {
                _isChangeEnabled = value;
                RaisePropertyChanged(nameof(IsChangeEnabled));
            }
        }

        public bool HasFreeSpots(string TourName, int Tourists)
        {
            TourService tourService = new TourService();
            Tour tour = tourService.GetByName(TourName);

            if (tour == null)
            {
                return false;
            }

            if (tourService.GetFreeSpotsNumber(tour.TourId) < Tourists)
            {
                return false;
            }
            return true;
        }

        private bool CanBook()
        {
            return !string.IsNullOrWhiteSpace(TourName) && Tourists > 0;
        }

        private bool CanChange()
        {
            if (!HasFreeSpots(TourName, Tourists) && CanBook())
            {
                return true;
            }
            return false;
        }

        private void ChangeTouristsNumber()
        {
            Tourists = 0;
        }

        private void ChooseTour()
        {
            return;
        }

        private void CancelReservation()
        {
            var loginWindow = new LoginWindow();
            CloseAction();
            loginWindow.Show();
        }

        private bool CanCancel()
        {
            if (CanChange())
            {
                    return true;
            }
            return false;

        }

        private void Book()
        {
            if (HasFreeSpots(TourName, Tourists))
            {
                NavigateToConfirmationWindow();
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void NavigateToConfirmationWindow()
        {
            ConfirmReservationWindow confirmReservationWindow = new ConfirmReservationWindow();
            ConfirmReservationViewModel confirmReservationViewModel = (ConfirmReservationViewModel)confirmReservationWindow.DataContext;
            confirmReservationViewModel.TourName = this.TourName;
            confirmReservationViewModel.Tourists = this.Tourists;
            confirmReservationWindow.Show();
        }

        public Action CloseAction { get; set; }

    }
}
