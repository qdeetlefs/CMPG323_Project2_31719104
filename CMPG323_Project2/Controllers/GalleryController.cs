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
    }
}
