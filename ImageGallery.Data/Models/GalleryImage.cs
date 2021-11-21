using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Data.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Url { get; set; }

        public string Geolocation { get; set; }

        public DateTime CapturedDate { get; set; }

        public string CapturedBy { get; set; }

        public virtual IEnumerable<ImageTag> Tags { get; set; }
    }
}
