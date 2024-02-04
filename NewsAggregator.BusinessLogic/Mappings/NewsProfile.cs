using AutoMapper;
using NewsAggregator.BusinessLogic.DTO;
using NewsAggregator.DataAccess.Entities;

namespace NewsAggregator.BusinessLogic.Mappings
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDto>();
            CreateMap<NewsDto, News>();
        }
    }
}
