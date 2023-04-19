using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace InitialProject.Repository
{
    public class TouristsRepository
    {
        public TouristsRepository() { }

        public Tourist GetById(int id)
        {
            using (var db = new DataContext())
            {
                return db.Tourists.FirstOrDefault(t => t.Id == id);
            }
        }

        public List<Tourist> GetTourists(int tourId)
        { 
            List<Tourist> tourists = new List<Tourist>();
            using (var db = new DataContext())
            {
                var tour = db.Tours.Include(t => t.Tourists).SingleOrDefault(t => t.TourId == tourId);
                if (tour != null)
                {
                    tourists.AddRange(tour.Tourists);
                }
            }
            return tourists;
        }

        public List<Coupon> GetTouristCoupons(int touristId)
        {
            using (var db = new DataContext())
            {
                CouponRepository couponRepository = new CouponRepository();
                List<Coupon> coupons = new List<Coupon>();
                couponRepository.RemoveExpiredCoupons();
                var tourist = db.Tourists.Include(t => t.Coupons).SingleOrDefault(t => t.Id == touristId);
                
                if (tourist != null)
                {
                    coupons.AddRange(tourist.Coupons);
                }

                return coupons;
            }
        }

        public List<Coupon> GetUsableTouristCoupons(int touristId)
        {
            using (var db = new DataContext())
            {
                CouponRepository couponRepository = new CouponRepository();
                List<Coupon> availableCoupons = new List<Coupon>();
                couponRepository.RemoveExpiredCoupons();
                var tourist = db.Tourists.Include(t => t.Coupons).SingleOrDefault(t => t.Id == touristId);

                if (tourist != null)
                {
                    foreach (Coupon coupon in tourist.Coupons)
                    {
                        if (coupon.IsUsed == false)
                        {
                            availableCoupons.Add(coupon);
                        }
                    }
                }

                return availableCoupons;
            }
        }
        public void AddCoupon(int touristId, Coupon coupon)
        {
            using (var db = new DataContext())
            {
                var tourist = db.Tourists.Include(t => t.Coupons).FirstOrDefault(t => t.Id == touristId);

                if (tourist != null)
                {   
                    tourist.Coupons.Add(coupon);
                    db.SaveChanges();
                }
            }
        }
    }
}
