using APINews.Services;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;
using NewsAPI.Services;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly ILogger<GalleryController> _logger;
        private readonly GalleryService _galleryServiceService;
        
        public GalleryController(ILogger<GalleryController> logger, GalleryService galleryServiceService)
        {
            _logger = logger;
            _galleryServiceService = galleryServiceService;
        }
        
        [HttpGet("{page}/{pageSize}")]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int pageSize) => 
            _galleryServiceService.GetGallery(page, pageSize);
        
        [HttpGet("{id:length(24)}", Name = "GetGallery")]
        public ActionResult<GalleryViewModel> Get(string id)
        {
            var gallery = _galleryServiceService.GetGalleryById(id);
            
            if (gallery == null) return NotFound();

            return gallery;
        }
        
        [HttpPost]
        public ActionResult<GalleryViewModel> Create(GalleryViewModel gallery)
        {
            var result =_galleryServiceService.CreateGallery(gallery);
            return CreatedAtRoute("GetGallery", new { id = result.Id }, result);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<GalleryViewModel> Update(string id, GalleryViewModel galleryViewModel)
        {
            var gallery = _galleryServiceService.GetGalleryById(id);
            
            if (gallery == null) return NotFound();
            
            galleryViewModel.PublishDate = gallery.PublishDate;
            
            _galleryServiceService.Update(id, galleryViewModel);
            
            return CreatedAtRoute("GetVideos", new { id = gallery.Id }, galleryViewModel);
        }
        
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var gallery = _galleryServiceService.GetGalleryById(id);

            if (gallery == null) return NotFound();

            _galleryServiceService.Remove(gallery.Id);
            
            var result = new
            {
                message = "Gallery deleted successfully",
                gallery
            };

            return Ok(result);
        }
    }
}
