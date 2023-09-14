using NewsAPI.Entities.Enums;

namespace NewsAPI.Entities.ViewModels;

public class VideoViewModel
{
    public string? Id { get; set; }
    
    public string? Hat { get;  set; }
        
    public string? Title { get; set; }
    
    public string? Author { get; set; }
        
    public string? Thumbnail { get; set; }
        
    public string? UrlVideo { get; set; }
        
    public Status Status { get; set; }
}