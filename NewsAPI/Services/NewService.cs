using AutoMapper;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;
using NewsAPI.Infra.Interfaces;
using NewsAPI.Services.Interfaces;

namespace NewsAPI.Services
{
    public class NewService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<News> _newsRepository;
        private readonly ICacheService _cacheService;
        private const string KeyService = "gallery"; 
        
        public NewService(IMapper mapper, IMongoRepository<News> newsRepository, ICacheService cacheService)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
            _cacheService = cacheService;
        }

        public Result<NewsViewModel> Get(int page, int pageSize)
        {
            var keyCache = $"{KeyService}_{page}_{pageSize}";
            var news = _cacheService.Get<Result<NewsViewModel>>(keyCache);
            
            if (news is not null) return news;
            news = _mapper.Map<Result<NewsViewModel>>(_newsRepository.Get(page, pageSize));
            _cacheService.Set(keyCache, news);
            
            return news;
        }

        public NewsViewModel GetNewsById(string id)
        {
            var keyCache = $"{KeyService}_{id}";
            var news = _cacheService.Get<NewsViewModel>(keyCache);
            
            if (news is not null) return news;
            news = _mapper.Map<NewsViewModel>(_newsRepository.Get(id));
            _cacheService.Set(keyCache, news);
            
            return news;
        }

        public NewsViewModel GetBySlug(string slug)
        {
            var keyCache = $"{KeyService}_{slug}";
            var news = _cacheService.Get<NewsViewModel>(keyCache);
            
            if (news is not null) return news;
            news = _mapper.Map<NewsViewModel>(_newsRepository.GetBySlug(slug));
            _cacheService.Set(keyCache, news);
            
            return news;
        }
        
        public NewsViewModel Create(NewsViewModel news)
        {
            var newsEntity = new News(news.Hat, news.Title, news.Text, news.Author, news.Img,
                news.Status);
            _newsRepository.Create(newsEntity);
            
            var keyCache = $"{KeyService}_{newsEntity.Slug}";
            _cacheService.Set(keyCache, newsEntity);
            
            return GetNewsById(newsEntity.Id);
        }

        public void Update(string id, NewsViewModel news)
        {
            var keyCache = $"{KeyService}_{id}";
            _newsRepository.Update(id, _mapper.Map<News>(news));
            
            _cacheService.Remove(keyCache);
            _cacheService.Set(keyCache, news);
        }

        public void Remove(string id)
        {
            var keyCache = $"{KeyService}_{id}";
            _newsRepository.Remove(keyCache);

            var news = GetNewsById(id);
            keyCache = $"{KeyService}_{news.Slug}";
            _newsRepository.Remove(keyCache);
            
            _cacheService.Remove(keyCache);
        }
      
    }
}