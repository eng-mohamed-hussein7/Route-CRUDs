using Application.Helpers;
using Application.ImageUploader;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.mageUploader;

public class ImageUploader : IImageUploader
{
    private readonly URLs _urls;

    public ImageUploader(IOptions<URLs> options)
    {
        _urls = options.Value;
    }

    public async Task<string?> UploadImageAsync(IFormFile image)
    {
        if (image == null || image.Length == 0)
            return null;

        var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var fullPath = Path.Combine(rootPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        return $"{_urls.ImageUploader}{fileName}";
    }

    public async Task<List<string>?> UploadImagesAsync(List<IFormFile> images)
    {
        if (images == null || !images.Any())
            return null;

        var uploadedUrls = new List<string>();
        var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);

        foreach (var image in images)
        {
            if (image == null || image.Length == 0)
                continue;

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var fullPath = Path.Combine(rootPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            uploadedUrls.Add($"{_urls.ImageUploader}{fileName}");
        }

        return uploadedUrls;
    }
}
