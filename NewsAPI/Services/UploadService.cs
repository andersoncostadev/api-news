using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using NewsAPI.Entities;
using NewsAPI.Entities.Enums;

namespace NewsAPI.Services;

public class UploadService
{
    public string UploadFile(IFormFile file)
    {
        var validateTypeMedia = GetTypeMedia(file.FileName);
        return validateTypeMedia == Media.Image ? UploadImage(file) : UploadVideo(file);
    }
    
    public Media GetTypeMedia(string fileName)
    {
        string[] imageExtensions = { ".jpg", ".png", ".webp", ".jpeg" };
        
        string[] videoExtensions = { ".mp4", ".webm", ".avi" };
        
        var fileInfo = new FileInfo(fileName);

        return imageExtensions.Contains(fileInfo.Extension) ? Media.Image :
            videoExtensions.Contains(fileInfo.Extension) ? Media.Video :
            throw new DomainException("Tipo de arquivo inválido para upload");
    }
    
    private string UploadImage(IFormFile file)
    {
        using (var stream = new FileStream(Path.Combine("Medias/Images", file.FileName), FileMode.Create))
        {
            file.CopyTo(stream);
        }
        
        var urlfile = Guid.NewGuid() + ".webp";
        
        using (var webPFileStream = new FileStream(Path.Combine("Medias/Images", urlfile), FileMode.Create))
        {
            using ( var imageFactory= new ImageFactory(preserveExifData: false))
            {
                imageFactory.Load(file.OpenReadStream())
                    .Format(new WebPFormat())
                    .Quality(100)
                    .Save(webPFileStream);
            }
        }
        
        return $"https://localhost:44325/medias/images/{urlfile}";
    }
    
    private string UploadVideo(IFormFile file)
    {
        var fi = new FileInfo(file.FileName);
        
        var fileName = Guid.NewGuid() + fi.Extension;
        
        using (var stream = new FileStream(Path.Combine("Medias/Videos", fileName), FileMode.Create))
        {
            file.CopyTo(stream);
        }
        
        return $"https://localhost:44325/medias/videos/{fileName}";
    }
}