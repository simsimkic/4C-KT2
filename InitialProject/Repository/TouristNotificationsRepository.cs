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
    public class TouristNotificationsRepository : ITouristNotificationsRepository
    {
        public TouristNotificationsRepository()
        {
            
        }

        public void Delete(TouristNotifications touristNotifications)
        {
            using (var db = new DataContext())
            {
                db.TouristNotifications.Remove(touristNotifications);
                db.SaveChanges();
            }
        }
        public List<TouristNotifications> GetAll()
        {
            using (var db = new DataContext())
            {
                return db.TouristNotifications.ToList();
            }
        }

        public List<TouristNotifications> GetByTourist(int touristId)
        {
            using (var db = new DataContext())
            {
                var touristToReturn = db.Tourists.Include(t => t.TouristNotifications).FirstOrDefault(t => t.Id == touristId);

                List<TouristNotifications> touristNotifications = new List<TouristNotifications>();
                touristNotifications.AddRange(touristToReturn.TouristNotifications);
                return touristNotifications;
            }
        }
    }
}
