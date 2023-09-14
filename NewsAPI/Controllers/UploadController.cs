using APINews.Services;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Services;

namespace NewsAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly ILogger<UploadController> _logger;
    private readonly UploadService _uploadService;

    public UploadController(ILogger<UploadController> logger, UploadService uploadService)
    {
        _logger = logger;
        _uploadService = uploadService;
    }

    [HttpPost]
    public IActionResult Post(IFormFile file)
    {
        try
        {
            if (file == null) return null;

           var urlFile = _uploadService.UploadFile(file);

            return Ok(new
            {
                menssagem = "Arquivo salvo com sucesso!",
                urlImagem = urlFile
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(500, new { error = e.Message });
        }
    }
}