using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG323_Project2.Models
{
    public class GalleryDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Url { get; set; }

        public string Geolocation { get; set; }

        public DateTime CapturedDate { get; set; }

        public string CapturedBy { get; set; }

        public List<string> Tags { get; set; }
    }
}
