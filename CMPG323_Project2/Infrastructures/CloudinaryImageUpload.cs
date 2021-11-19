using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CMPG323_Project2.Models;
using ImageGallery.Data.Models;
using ImageGallery.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG323_Project2.Infrastructures
{
    public class CloudinaryImageUpload : ICloudinaryImageUpload
    {

        private string ApiKey { get; set; }
        private string ApiSecret { get; set; }
        private string Cloud { get; set; }
        private Account Account { get; set; }


        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageGalleryDbContext _ctx;

        public CloudinaryImageUpload(IConfiguration configuration, IWebHostEnvironment webHostEnvironment,
            ImageGalleryDbContext ctx)
        {
            this.ApiKey = configuration["Cloudinary:ApiKey"];
            this.ApiSecret = configuration["Cloudinary:ApiSecret"];
            this.Cloud = configuration["Cloudinary:Cloud"];
            this.Account = new Account { ApiKey = this.ApiKey, ApiSecret = this.ApiSecret, Cloud = this.Cloud };
            this.configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _ctx = ctx;
        }

        public async Task<string> UploadPicture(UploadImageModel model)
        {
            var cloudinary = new Cloudinary(Account);
            cloudinary.Api.Secure = true;

            //reads the Image in the IFormFile into a bytes then we convert    this to a base64 string
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                model.UploadImage.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            string base64 = Convert.ToBase64String(bytes);


            var prefix = @"data:image/png;base64,";
            var imagePath = prefix + base64;

            // create a new ImageUploadParams object and assign the directory name
            var uploadParams = new ImageUploadParams()

            {
                File = new FileDescription(imagePath),
                Folder = "Images"
            };
            // pass the new ImageUploadParams object to the UploadAsync method of the Cloudinary Api


            var uploadResult = await cloudinary.UploadAsync(@uploadParams);

            // adds the new image to be uploaded to the database
            var image = new GalleryImage()
            {
                Title = model.Title,
                Created = DateTime.Now,
                Url = uploadResult.Url.AbsoluteUri,
                Tags = ParseTags(model.Tags)

            };
            _ctx.Add(image);
            await _ctx.SaveChangesAsync();

            return uploadResult.SecureUrl.AbsoluteUri;
        }

     


        private List<ImageTag> ParseTags(string tags) 
        {
            return tags.Split(",").Select(tag => new ImageTag
            {
                Description = tag,

            }).ToList();

           

        }
    }
}
