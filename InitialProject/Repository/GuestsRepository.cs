using InitialProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class GuestsRepository
    {
        public GuestsRepository() { }
        
        public List<Guest> GetGuests(int accId)
        {
            List<Guest> guests = new List<Guest>();
            using(var db = new DataContext())
            {
            
                var acc = db.Accomodations.Include(a => a.Guests).SingleOrDefault(a => a.AccId == accId);
                if(acc != null)
                {
                    guests.AddRange(acc.Guests);
                }
            }
            return guests;
        }
    }
}
