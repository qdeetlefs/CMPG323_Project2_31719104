using CMPG323_Project2.Models;
using ImageGallery.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG323_Project2.Controllers
{
    public class GalleryController : Controller
    {

        private readonly IImageService _iImageService;

        public GalleryController(IImageService iImageService)
        {
            _iImageService = iImageService;
        }


        public IActionResult Index()
        {
            var imageList = _iImageService.GetAll();
            var model = new GalleryIndexModel()
            {
                Images = imageList,
                SearchQuery = ""
            };
            return View(model);

        }

        public IActionResult Detail(int id)
        {
            var image = _iImageService.GetById(id);
            var model = new GalleryDetailModel()
            {
                Id = image.Id,
                Created = image.Created,
                Url = image.Url,
                Title = image.Title,
                Tags = image.Tags.Select(x => x.Description).ToList(),
                Geolocation = image.Geolocation,
                CapturedBy = image.CapturedBy,
                CapturedDate = image.CapturedDate
            };

            return View(model);


        }


    }
}
