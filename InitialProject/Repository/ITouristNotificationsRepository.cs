using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public interface ITouristNotificationsRepository
    {
        public void Delete(TouristNotifications touristNotifications);
        public List<TouristNotifications> GetAll();
        public List<TouristNotifications> GetByTourist(int touristId);

    }
}
