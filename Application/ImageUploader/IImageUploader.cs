using Microsoft.AspNetCore.Http;

namespace Application.ImageUploader;

public interface IImageUploader
{
    Task<string?> UploadImageAsync(IFormFile image);
}
