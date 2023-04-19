using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace InitialProject.Service
{
    public class KT2_F1_Guide
    {   
        TourRepository tourRepository = new TourRepository();
        DatesRepository datesRepository = new DatesRepository();
        TouristsRepository touristsRepository = new TouristsRepository();
        public KT2_F1_Guide() { }

        public bool CancelTour(int dateId)
        {
            Dates date = datesRepository.GetById(dateId);
            //Dates dateToCancel = PickTour(tour, date.Date);
            if(CheckDate(date))
            {
                AssignCoupon(date);
                datesRepository.Delete(date);
                return true;
            }
            return false;
        }

        public Dates PickTour(Tour tourToCancel, DateTime tourDate)
        {
            
            foreach(var date in tourToCancel.StartingDates) 
            {
                if(date.Date.Equals(tourDate)) return date;
            }

            return null;
        }

        public bool CheckDate(Dates date)
        {   
            DateTime todaysDate = DateTime.Now;
            if((date.Date - todaysDate).TotalHours >=48)
            {
                return true;
            }

            return false;
        }

        public void AssignCoupon(Dates tourDate)
        {
            foreach(var tourist in tourDate.Tourists)
            {
                Coupon coupon = new Coupon();
                
                touristsRepository.AddCoupon(tourist.Id, coupon);
                
            } 
        }
    }
}
