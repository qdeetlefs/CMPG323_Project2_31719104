using ImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Data
{
    public interface IImageService
    {
        IEnumerable<GalleryImage> GetAll();
        IEnumerable<GalleryImage> GetWithTag(string tag);
        GalleryImage GetById(int id);
    }
}
