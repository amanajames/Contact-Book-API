using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration config;
        private readonly Cloudinary cloudinary;
        private readonly ImageUploadSettings  _accountSettings;
        public ImageService(IConfiguration config, IOptions<ImageUploadSettings> accountSettings)
        {
            _accountSettings = accountSettings.Value;
            this.config = config;
            cloudinary = new Cloudinary(new Account(_accountSettings.CloudName, _accountSettings.ApiKey, _accountSettings.ApiSecret));
        }
        public async Task<UploadResult> UploadAsync(IFormFile image)
        {
            var pictureFormat = false;
            var listOfImageExtentions = config.GetSection("PhotoSettings:Formats").Get<List<string>>();
            foreach (var item in listOfImageExtentions)
            {
                if (image.FileName.EndsWith(item))
                {
                    pictureFormat = true;
                    break;
                }
            }

            if (pictureFormat == false)
            {
                throw new ArgumentException("File format not supported");
            }

            var uploadResult = new ImageUploadResult();
            using (var imageStream = image.OpenReadStream())
            {
                string filename = Guid.NewGuid().ToString() + image.FileName;

                uploadResult = await cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(filename, imageStream),
                    Transformation = new Transformation().Crop("thumb").Gravity("face").Width(150).Height(150)
                });
            }
            return uploadResult;
        }
    }
}
