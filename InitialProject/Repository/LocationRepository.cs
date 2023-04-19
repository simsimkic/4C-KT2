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
    public class LocationRepository
    {
        public LocationRepository() { }
        public void AddLocation(Location locationToAdd)
        {
            using (var db = new DataContext())
            {
                db.Locations.Add(locationToAdd);
                db.SaveChanges();
            }
        }

        public List<Location> GetAllLocations()
        {
            using (var db = new DataContext())
            {
                return db.Locations.ToList();
            }
        }

        public void DeleteLocation(int id)
        {
            using (var db = new DataContext())
            {
                var locationToDelete = db.Locations.FirstOrDefault(t => t.LocationId == id);

                if (locationToDelete != null)
                {
                    db.Locations.Remove(locationToDelete);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateLocation(int id, Location locationToUpdate)
        {
            using (var db = new DataContext())
            {
                var location = db.Locations.FirstOrDefault(t => t.LocationId == id);

                if (location != null)
                {
                    location.City = locationToUpdate.City;
                    location.Country = locationToUpdate.Country;
                    location.Tours = locationToUpdate.Tours;
                    location.Accomodations = locationToUpdate.Accomodations;
                    db.SaveChanges();
                }
            }
        }

        public Location GetLocationById(int id)
        {
            using (var db = new DataContext())
            {
                return db.Locations.FirstOrDefault(t => t.LocationId == id);
            }
        }

        public Location GetByCityAndCountry(string city, string country)
        {
            using (var db = new DataContext())
            {
                foreach (Location loc in db.Locations)
                {
                    if (loc.City == city && loc.Country == country)
                    {
                        return loc;
                    }
                }
            }
            return null;
        }
    }

}