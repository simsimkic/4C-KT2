using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourImages
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public TourImages() { }

        public TourImages(int id, string name, string url) 
        {
            Id = id;
            Name = name;
            URL = url;
        }

        public TourImages(string name, string url)
        {
            Name = name;
            URL = url;
        }
    }
}
