using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace InitialProject.Service
{
    public class LocationService
    {

        LocationRepository locationRepository = new LocationRepository();

        public LocationService()
        {

        }

        public List<Location> GetAllLocations()
        {
            return locationRepository.GetAllLocations();
        }

        public Location GetByCityAndCountry(string city, string country)
        {
            return locationRepository.GetByCityAndCountry(city, country);
        }

    }
}
