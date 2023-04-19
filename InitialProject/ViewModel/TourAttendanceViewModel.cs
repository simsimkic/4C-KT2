using InitialProject.Commands;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WebApi.Entities;

namespace InitialProject.ViewModel
{
    public class TourAttendanceViewModel : INotifyPropertyChanged
    {
        private string _tourName;
        private TourAttendanceDTO _tourAttendance;

        private ObservableCollection<Checkpoint> _checkpoints;

        private CheckpointService checkpointService = new CheckpointService();

        private TourService tourService = new TourService();

        public ICommand ShowCheckpointsCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TourAttendanceViewModel()
        {
            ShowCheckpointsCommand = new DelegateCommand(ShowCheckpoints);
        }

        public string TourName
        {
            get { return _tourName; }
            set { _tourName = value; RaisePropertyChanged(nameof(TourName)); }
        }
        public TourAttendanceDTO TourAttendance
        {
            get { return _tourAttendance; }
            set { _tourAttendance = value; RaisePropertyChanged(nameof(TourAttendance)); }
        }

        public ObservableCollection<Checkpoint> Checkpoints
        {
            get { return _checkpoints; }
            set
            {
                _checkpoints = value;
                RaisePropertyChanged(nameof(Checkpoints));
            }
        }

        public void ShowCheckpoints()
        {
            Tour tour = tourService.GetByName(TourName);
            if (tour == null)
            {
                MessageBox.Show("No tour");
            }
            Checkpoints = new ObservableCollection<Checkpoint>(checkpointService.GetTourCheckpoints(tour.TourId));
        }


        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Action CloseAction { get; set; }

    }
}
