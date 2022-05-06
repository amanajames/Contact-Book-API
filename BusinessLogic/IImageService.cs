using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Models.DTOS;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IImageService
    {
        Task<UploadResult> UploadAsync(IFormFile image);
    }
}