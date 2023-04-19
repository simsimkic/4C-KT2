using InitialProject.Commands;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using InitialProject.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WebApi.Entities;

namespace InitialProject.ViewModel
{
    public class ConfirmReservationViewModel : INotifyPropertyChanged
    {
        private string _tourName;
        private int _tourists;
        private bool _isCouponEnabled;
        private int _couponId;

        private ObservableCollection<Coupon> _coupons;

        TouristsService touristsService = new TouristsService();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CancelReservationCommand { get; set; }
        public ICommand ConfirmReservationCommand { get; set; }
        public ICommand UseCouponCommand { get; set; }

        public ConfirmReservationViewModel()
        {
            CancelReservationCommand = new DelegateCommand(CancelReservation);
            ConfirmReservationCommand = new DelegateCommand(ConfirmReservation);
            UseCouponCommand = new DelegateCommand(UseCoupon);

            Coupons = new ObservableCollection<Coupon>(touristsService.GetUsableTouristCoupons(UserSession.LoggedInUser.Id));
            IsCouponEnabled = true;

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

        public int CouponId
        {
            get { return _couponId; }
            set
            {
                _couponId = value;
                RaisePropertyChanged(nameof(CouponId));
            }
        }

        public bool IsCouponEnabled
        {
            get { return _isCouponEnabled; }
            set
            {
                _isCouponEnabled = value;
                RaisePropertyChanged(nameof(IsCouponEnabled));
            }
        }

        public ObservableCollection<Coupon> Coupons
        {
            get { return _coupons; }
            set
            {
                _coupons = value;
                RaisePropertyChanged(nameof(Coupons));
            }
        }

        private void CancelReservation()
        {
            CloseAction();
        }

        public void ConfirmReservation()
        {

            TourService tourService = new TourService();
            Tour tour = tourService.GetByName(TourName);
            tourService.BookTour(tour.TourId, UserSession.LoggedInUser.Id, Tourists);
            if (IsCouponEnabled == false)
            {
                CouponService couponService = new CouponService();
                couponService.UseCoupon(CouponId);
            }
            MessageBox.Show("You successfully booked a tour!");
            Cancel();
        }

        public bool IsCouponExpired(Coupon coupon)
        {
            if (coupon.Date < DateTime.Now)
            {
                CouponService couponService = new CouponService();
                couponService.Delete(coupon);
                Coupons = new ObservableCollection<Coupon>(touristsService.GetUsableTouristCoupons(UserSession.LoggedInUser.Id));
                return true;
            }
            return false;
        }

        public void UseCoupon(object parameter)
        {
            Coupon coupon = (Coupon)parameter;

            if (IsCouponExpired(coupon))
            {
                MessageBox.Show("Coupon expired");
            } else
            {
                IsCouponEnabled = false;
                CouponId = coupon.Id;
                Coupons = new ObservableCollection<Coupon>(touristsService.GetUsableTouristCoupons(UserSession.LoggedInUser.Id));
            }
            
        }


        private void Cancel()
        {
            CloseAction();
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Action CloseAction { get; set; }

    }
}
