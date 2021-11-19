using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG323_Project2.Models
{
    public class UploadImageModel
    {
        public string Title { get; set; }

        [FileExtensions(Extensions = "bmp,ico,jpeg,jpg,gif,tiff,png")]
        public IFormFile UploadImage { get; set; }

        public string Tags { get; set; }

    }
}
