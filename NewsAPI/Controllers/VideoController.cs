using APINews.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;

namespace NewsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        
        private readonly ILogger<VideoController> _logger;
        private readonly VideoService _videoService;

        public VideoController(ILogger<VideoController> logger, VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }
        
        [HttpGet]
        public ActionResult<Result<VideoViewModel>> Get(int page, int pageSize) => _videoService.Get(page, pageSize);
        
        [HttpGet("{id:length(24)}", Name = "GetVideos")]
        public ActionResult<VideoViewModel> Get(string id)
        {
            var video = _videoService.GetVideoById(id);
            
            if (video == null) return NotFound();

            return video;
        }
        
        [HttpPost]
        public ActionResult<VideoViewModel> Create(VideoViewModel video)
        {
            var result =_videoService.Create(video);
            return CreatedAtRoute("GetVideos", new { id = result.Id }, result);
        }
        
        [HttpPut("{id:length(24)}")]
        public ActionResult<VideoViewModel> Update(string id, VideoViewModel videoIn)
        {
            var video = _videoService.GetVideoById(id);

            if (video == null) return NotFound();

            _videoService.Update(id, videoIn);

            return CreatedAtRoute("GetVideos", new { id = video.Id }, videoIn);
        }
        
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var video = _videoService.GetVideoById(id);

            if (video == null) return NotFound();

            _videoService.Remove(video.Id);

            var result = new
            {
                message = "Video removido com sucesso!"
            };

            return Ok(result);
        }
    }
}
