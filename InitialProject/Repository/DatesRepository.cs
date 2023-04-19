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
    public class DatesRepository
    {
        public DatesRepository() { }   

        public void Delete(Dates date)
        {
            using(var db = new DataContext())
            {
                 db.Dates.Remove(date);
                 db.SaveChanges();
            }
        }
         
        public List<Dates> GetAll ()
        {
            using (var db = new DataContext())
            {
                return db.Dates.Include(t=>t.Tourists).ToList();
            }
        }
        
        public Dates GetById(int id)
        {
            using (var db = new DataContext())
            {
                return  db.Dates.Include(d => d.Tourists)
                        .ThenInclude(t => t.Coupons)
                        .FirstOrDefault(d => d.Id == id);
            }

            
        }

        public Dates GetByIdRatings(int id)
        {
            using (var db = new DataContext())
            {
                return db.Dates.Include(d => d.Tourists)
                        .ThenInclude(t => t.TourRatings)
                        .FirstOrDefault(d => d.Id == id);
            }


        }

        public List<Dates> GetByYear(int year)
        {
            using (var db = new DataContext())
            {
                return db.Dates.Include(t => t.Tourists).Where(t=>t.Date.Year == year).ToList();
            }
        }
    }
}
