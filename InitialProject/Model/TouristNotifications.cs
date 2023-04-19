using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TouristNotifications
    {
        [Key]
        public int Id { get; set; }

        public TouristNotifications() { }

    }
}