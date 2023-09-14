using NewsAPI.Entities.Enums;

namespace NewsAPI.Entities.ViewModels;

public class GalleryViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    
    public string? Legend { get;  set; }
    
    public string? Author { get; set; }
    
    public string? Tags { get; set; }
    
    public string? Thumbnail { get; set; }
    
    public IList<string>? GalleryImages { get; set; }
    
    public Status Status { get;  set; }
    
    public string? Slug { get; set; }
    
    public DateTime PublishDate { get; set; }
}