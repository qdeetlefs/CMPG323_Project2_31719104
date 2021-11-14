﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Data.Models
{
    public class ImageTag
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string Geolocation { get; set; }

        public DateTime CaptureDate { get; set; }

        public string CapturedBy { get; set; }
    }
}
