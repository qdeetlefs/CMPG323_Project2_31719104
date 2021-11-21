using CMPG323_Project2.Models;
using ImageGallery.Data;
using ImageGallery.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG323_Project2.Controllers
{
    public class GalleryController : Controller
    {

        private readonly IImageService _iImageService;
        private readonly ImageGalleryDbContext _ctx;

  
        public GalleryController(IImageService iImageService, ImageGalleryDbContext ctx)
        {
            _iImageService = iImageService;
            _ctx = ctx;
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



        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var images = await _ctx.GalleryImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var images = await _ctx.GalleryImages.FindAsync(id);

            if (images.Url != null)
            {
                _ctx.GalleryImages.Remove(images);
                await _ctx.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }



        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var images = await _ctx.GalleryImages.FindAsync(id);
            if (images == null)
            {
                return NotFound();
            }
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Created,Url,Geolocation,CapturedDate,CapturedBy,Tags")] GalleryImage images)
        {
            if (id != images.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (images.Url != null)
                    {
                        _ctx.Update(images);
                        await _ctx.SaveChangesAsync();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (id != (images.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(images);
        }
    }
}
