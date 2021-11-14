using ImageGallery.Data;
using ImageGallery.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Services
{
    public class ImageService : IImageService
    {
        private readonly ImageGalleryDbContext _ctx;

        public ImageService(ImageGalleryDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<GalleryImage> GetAll()
        {
            return _ctx.GalleryImages.Include(x => x.Tags);
        }

        public GalleryImage GetById(int id)
        {
            return GetAll().Where(img => img.Id == id).First();

            //return _ctx.GalleryImages.Find(id);
        }

        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            return GetAll().Where(img => img.Tags.Any(t => t.Description == tag));
        }


    }
}
