using InitialProject.Commands;
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

namespace InitialProject.ViewModel
{
    public class TouristCouponsViewModel : INotifyPropertyChanged
    {
        TouristsService touristsService = new TouristsService();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RefreshCommand { get; set; }

        public TouristCouponsViewModel()
        {
            Coupons = new ObservableCollection<Coupon>(touristsService.GetTouristCoupons(UserSession.LoggedInUser.Id));
            RefreshCommand = new DelegateCommand(Refresh);
        }


        private ObservableCollection<Coupon> _coupons;

        public ObservableCollection<Coupon> Coupons
        {
            get { return _coupons; }
            set
            {
                _coupons = value;
                RaisePropertyChanged(nameof(Coupons));
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh()
        {
            Coupons = new ObservableCollection<Coupon>(touristsService.GetTouristCoupons(UserSession.LoggedInUser.Id));
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action CloseAction { get; set; }
    }
}
