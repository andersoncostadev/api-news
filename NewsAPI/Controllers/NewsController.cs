using APINews.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;
using NewsAPI.Services;

namespace NewsAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly ILogger<NewsController> _logger;
    private readonly NewService _newService;

    public NewsController(ILogger<NewsController> logger, NewService newService)
    {
        _logger = logger;
        _newService = newService;
    }
        
    [HttpGet]
    public ActionResult<Result<NewsViewModel>> Get(int page, int pageSize) => _newService.Get(page, pageSize);
        
    [HttpGet("{id:length(24)}", Name = "GetNews")]
    public ActionResult<NewsViewModel> Get(string id)
    {
        var news = _newService.GetNewsById(id);
            
        if (news == null) return NotFound();

        return news;
    }
        
    [HttpPost]
    public ActionResult<NewsViewModel> Create(NewsViewModel news)
    {
        var result =_newService.Create(news);
        return CreatedAtRoute("GetNews", new { id = result.Id }, result);
    }
        
    [HttpPut("{id:length(24)}")]
    public ActionResult<NewsViewModel> Update(string id, NewsViewModel newsIn)
    {
        var news = _newService.GetNewsById(id);

        if (news == null) return NotFound();

        _newService.Update(id, newsIn);

        return CreatedAtRoute("GetNews", new { id = news.Id }, newsIn);
    }
        
    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var news = _newService.GetNewsById(id);

        if (news == null) return NotFound();

        _newService.Remove(news.Id);

        var result = new
        {
            message = "Notícia removida com sucesso!"
        };

        return Ok(result);
    }
}