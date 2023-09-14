using APINews.Services;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;
using NewsAPI.Services;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryExternalController : ControllerBase
    {
        private readonly ILogger<GalleryExternalController> _logger;
        private readonly GalleryService _galleryServiceService;

        public GalleryExternalController(ILogger<GalleryExternalController> logger, GalleryService galleryServiceService)
        {
            _logger = logger;
            _galleryServiceService = galleryServiceService;
        }
        
        [HttpGet("{page}/{pageSize}")]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int pageSize) => 
            _galleryServiceService.GetGallery(page, pageSize);
        
        
        [HttpGet("{slug}")]
        public ActionResult<GalleryViewModel> Get(string slug)
        {
            var gallery = _galleryServiceService.GetBySlug(slug);
            
            if (gallery == null) return NotFound();

            return gallery;
        }
    }
}
