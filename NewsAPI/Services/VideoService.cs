using AutoMapper;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;
using NewsAPI.Infra.Interfaces;
using NewsAPI.Services.Interfaces;

namespace APINews.Services;

public class VideoService
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<Video> _videoRepository;
    private readonly ICacheService _cacheService;
    private const string KeyService = "gallery"; 
        
    public VideoService(IMapper mapper, IMongoRepository<Video> newsRepository, ICacheService cacheService)
    {
        _mapper = mapper;
        _videoRepository = newsRepository;
        _cacheService = cacheService;
    }

    public Result<VideoViewModel> Get(int page, int pageSize)
    {
        var keyCache = $"{KeyService}_{page}_{pageSize}";
        var video = _cacheService.Get<Result<VideoViewModel>>(keyCache);
        
        if (video is not null) return video;
        video = _mapper.Map<Result<VideoViewModel>>(_videoRepository.Get(page, pageSize));
        _cacheService.Set(keyCache, video);
        
        return video;
    }

    public VideoViewModel GetVideoById(string id)
    {
        var keyCache = $"{KeyService}_{id}";
        var video = _cacheService.Get<VideoViewModel>(keyCache);
        
        if (video is not null) return video;
        video = _mapper.Map<VideoViewModel>(_videoRepository.Get(id));
        _cacheService.Set(keyCache, video);
        
        return video;
    }

    public VideoViewModel GetBySlug(string slug)
    {
        var keyCache = $"{KeyService}_{slug}";
        var video = _cacheService.Get<VideoViewModel>(keyCache);
        
        if (video is not null) return video;
        video = _mapper.Map<VideoViewModel>(_videoRepository.GetBySlug(slug));
        _cacheService.Set(keyCache, video);
        
        return video;
    }
    
    public VideoViewModel Create(VideoViewModel video)
    {
        var videoEntity = new Video(video.Hat, video.Title, video.Thumbnail, video.Author, video.UrlVideo,   
            video.Status);
        _videoRepository.Create(videoEntity);
        
        var keyCache = $"{KeyService}_{videoEntity.Slug}";
        _cacheService.Set(keyCache, videoEntity);
        
        return GetVideoById(videoEntity.Id);
    }

    public void Update(string id, VideoViewModel video)
    {
        var keyCache = $"{KeyService}_{id}";
        _videoRepository.Update(id, _mapper.Map<Video>(video));
        
        _cacheService.Remove(keyCache);
        _cacheService.Set(keyCache, video);
    }

    public void Remove(string id)
    {
        var keyCache = $"{KeyService}_{id}";
        _videoRepository.Remove(keyCache);

        var video = GetVideoById(id);
        keyCache = $"{KeyService}_{video.Id}";
        _videoRepository.Remove(keyCache);
        
        _cacheService.Remove(keyCache);
    }
      
}
