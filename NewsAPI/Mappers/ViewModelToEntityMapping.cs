using AutoMapper;
using NewsAPI.Entities;
using NewsAPI.Entities.ViewModels;

namespace NewsAPI.Mappers
{
    public class ViewModelToEntityMapping : Profile
    {
        public ViewModelToEntityMapping()
        {
             CreateMap<NewsViewModel, News>();
             CreateMap<VideoViewModel, Video>();
        }
    }
}