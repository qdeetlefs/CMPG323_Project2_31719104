using CMPG323_Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG323_Project2.Infrastructures
{
    public interface ICloudinaryImageUpload
    {

        public Task<string> UploadPicture(UploadImageModel model);
    }
}
