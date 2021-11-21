using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.Text)]
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$", ErrorMessage = "Coordinates are not valid.")]
        public string Geolocation { get; set; }

        public DateTime CapturedDate { get; set; }

        public string CapturedBy { get; set; }

        public List<string> Tags { get; set; }
    }
}
