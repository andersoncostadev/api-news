using APINews.Services;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;
using NewsAPI.Services;

namespace NewsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsExternalController : ControllerBase
{
    private readonly ILogger<NewsExternalController> _logger;
    private readonly NewService _newsServices;
    
    public NewsExternalController(ILogger<NewsExternalController> logger, NewService newsServices)
    {
        _logger = logger;
        _newsServices = newsServices;
    }
    
    [HttpGet]
    public ActionResult<Result<NewsViewModel>> Get(int page, int pageSize) => _newsServices.Get(page, pageSize);
    
    [HttpGet("{slug}")]
    public ActionResult<NewsViewModel> GetBySlug(string slug)
    {
        var news = _newsServices.GetBySlug(slug);
        
        if (news is null) return NotFound();
        
        return news;
    }
}