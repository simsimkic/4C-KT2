using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class TourNotificationsService
    {
        TouristNotificationsRepository touristNotificationsRepository = new TouristNotificationsRepository();
        public TourNotificationsService()
        {
        }

        public void Delete(TouristNotifications touristNotifications)
        {
            touristNotificationsRepository.Delete(touristNotifications);
        }

        public List<TouristNotifications> GetAll()
        {
            return touristNotificationsRepository.GetAll();
        }
        public List<TouristNotifications> GetByTourist(int touristId)
        {
            return touristNotificationsRepository.GetByTourist(touristId);
        }





    }
}
