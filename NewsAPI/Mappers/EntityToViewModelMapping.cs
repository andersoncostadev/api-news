using AutoMapper;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;

namespace NewsAPI.Mappers
{
    public class EntityToViewModelMapping : Profile
    {
        public EntityToViewModelMapping()
        {
            #region [Entities]
            CreateMap<News, NewsViewModel>().ReverseMap();
            CreateMap<Video, VideoViewModel>().ReverseMap();
            CreateMap<Gallery, GalleryViewModel>().ReverseMap();
            #endregion

            #region [Result<T>]
            CreateMap<Result<News>, Result<NewsViewModel>>().ReverseMap();
            CreateMap<Result<Video>, Result<VideoViewModel>>().ReverseMap();
            CreateMap<Result<Gallery>, Result<GalleryViewModel>>().ReverseMap();
            #endregion
        }
    }
}