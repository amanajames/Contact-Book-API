using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Models.DTOS;
using System;
using System.Threading.Tasks;

namespace Week10API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPatch]
        public async Task<IActionResult> UploadImage([FromForm] AddImageDTO addImageDto)
        { 
            try
            {
                var upload = await _imageService.UploadAsync(addImageDto.Image);
                var result = new ImageAddedDTO()
                {
                    publicId = upload.PublicId,
                    Url = upload.Url.ToString(),
                };

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message); 
            }
        }
    }
}
