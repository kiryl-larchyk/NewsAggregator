using NewsAggregator.BusinessLogic.DTO;
using NewsAggregator.BusinessLogic.Enums;

namespace NewsAggregator.BusinessLogic.Interfaces
{
    public interface INewsService
    {
        IEnumerable<NewsDto> GetNews();

        IEnumerable<NewsDto> GetNewsBySubstring(string substring, NewsFilterType filterType);

        Task AddNewsByRssKey(string rssKey);
    }
}
