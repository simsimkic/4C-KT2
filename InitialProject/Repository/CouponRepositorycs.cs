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
    public class CouponRepository
    {
        public CouponRepository() { }

        public void Delete(Coupon coupon)
        {
            using (var db = new DataContext())
            {
                db.Coupons.Remove(coupon);
                db.SaveChanges();
            }
        }

        public List<Coupon> GetAll()
        {
            using (var db = new DataContext())
            {
                return db.Coupons.ToList();
            }
        }

        public Coupon GetById(int id)
        {
            using (var db = new DataContext())
            {
                return db.Coupons.FirstOrDefault(t => t.Id == id);
            }
        }

        public void UseCoupon(int couponId)
        {
            using (var db = new DataContext())
            {
                var couponToUpdate = db.Coupons.FirstOrDefault(t => t.Id == couponId);
                couponToUpdate.IsUsed = true;
                db.Coupons.Update(couponToUpdate);
                db.SaveChanges();
            }
        }

        public void RemoveExpiredCoupons()
        {
            using (var db = new DataContext())
            {
                List<Coupon> coupons = GetAll();
                foreach (Coupon coupon in coupons)
                {
                    if (coupon.Date < DateTime.Now)
                    {
                        Delete(coupon);
                    }
                }
            }
        }
        
    }
}