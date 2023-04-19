using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class AccomodationImage
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public AccomodationImage() { }

        public AccomodationImage(string name, string url)
        {
            Name = name;
            URL = url;
        }

        public AccomodationImage(string name, string url)
        {
            Name = name;
            URL = url;
        }


    }
}
