using APINews.Services;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;

namespace NewsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoExternalController : ControllerBase
{
    private readonly ILogger<VideoExternalController> _logger;
    private readonly VideoService _videoServices;
    
    public VideoExternalController(ILogger<VideoExternalController> logger, VideoService videoServices)
    {
        _logger = logger;
        _videoServices = videoServices;
    }
    
    [HttpGet]
    public ActionResult<Result<VideoViewModel>> Get(int page, int pageSize) => _videoServices.Get(page, pageSize);
    
    [HttpGet("{slug}")]
    public ActionResult<VideoViewModel> GetBySlug(string slug)
    {
        var video = _videoServices.GetBySlug(slug);
        
        if (video is null) return NotFound();
        
        return video;
    }
}