using AutoMapper;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;
using NewsAPI.Infra.Interfaces;
using NewsAPI.Services.Interfaces;

namespace NewsAPI.Services;

public class GalleryService
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<Gallery> _galleryRepository;
    private readonly ICacheService _cacheService;
    private const string KeyService = "gallery";

    public GalleryService(IMapper mapper, IMongoRepository<Gallery> galleryRepository, ICacheService cacheService)
    {
        _mapper = mapper;
        _galleryRepository = galleryRepository;
        _cacheService = cacheService;
    }

    public Result<GalleryViewModel> GetGallery(int page, int pageSize)
    {
        var keyCache = $"{KeyService}_{page}_{pageSize}";
        var gallery = _cacheService.Get<Result<GalleryViewModel>>(keyCache);

        if (gallery is not null) return gallery;
        gallery = _mapper.Map<Result<GalleryViewModel>>(_galleryRepository.Get(page, pageSize));
        _cacheService.Set(keyCache, gallery);

        return gallery;
    }
    
    public GalleryViewModel GetGalleryById(string id)
    {
        var keyCache = $"{KeyService}_{id}";
        var gallery = _cacheService.Get<GalleryViewModel>(keyCache);
        
        if (gallery is not null) return gallery;
        gallery = _mapper.Map<GalleryViewModel>(_galleryRepository.Get(id));
        _cacheService.Set(keyCache, gallery);
        
        return gallery;
    }
    
    public GalleryViewModel GetBySlug(string slug)
    {
        var keyCache = $"{KeyService}_{slug}";
        var gallery = _cacheService.Get<GalleryViewModel>(keyCache);
        
        if (gallery is not null) return gallery;
        gallery = _mapper.Map<GalleryViewModel>(_galleryRepository.GetBySlug(slug));
        _cacheService.Set(keyCache, gallery);
        
        return gallery;
    }
      
    
    public GalleryViewModel CreateGallery(GalleryViewModel galleryViewModel)
    {
        var entity = new Gallery(
            galleryViewModel.Title, 
            galleryViewModel.Legend, 
            galleryViewModel.Author, 
            galleryViewModel.Tags, 
            galleryViewModel.Status, 
            galleryViewModel.GalleryImages, 
            galleryViewModel.Thumbnail,
            galleryViewModel.PublishDate
        );
        
        _galleryRepository.Create(entity);
        
        var keyCache = $"{KeyService}_{entity.Slug}";
        _cacheService.Set(keyCache, entity);
        
        return GetGalleryById(entity.Id);
    }

    public void Update(string id, GalleryViewModel galleryViewModel)
    {
        var keyCache = $"{KeyService}_{id}";
       _galleryRepository.Update(id, _mapper.Map<Gallery>(galleryViewModel));
       
       _cacheService.Remove(keyCache);
       _cacheService.Set(keyCache, galleryViewModel);
    }


    public void Remove(string id)
    {
        var keyCache = $"{KeyService}_{id}";
        _galleryRepository.Remove(keyCache);

        var gallery = GetGalleryById(id);
        keyCache = $"{KeyService}_{gallery.Slug}";
        _galleryRepository.Remove(keyCache);
        
        _cacheService.Remove(keyCache);
    }
}