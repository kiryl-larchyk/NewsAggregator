using AutoMapper;
using NewsAggregator.BusinessLogic.DTO;
using NewsAggregator.BusinessLogic.Enums;
using NewsAggregator.BusinessLogic.Interfaces;
using NewsAggregator.DataAccess.Entities;
using NewsAggregator.DataAccess.Interfaces;
using System.Xml;

namespace NewsAggregator.BusinessLogic.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<News> _newsRepository;
        private readonly IMapper _mapper;

        public NewsService(IRepository<News> newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }

        public async Task AddNewsByRssKey(string rssKey)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(rssKey);
            var rssNodes = xmlDocument.SelectNodes("rss/channel/item");
            foreach (XmlNode rssNode in rssNodes)
            {
                var rssSubNode = rssNode.SelectSingleNode("title");
                var title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                var description = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("link");
                var link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("pubDate");
                var publicationDate = rssSubNode != null ? rssSubNode.InnerText : "";

                await _newsRepository.Create(new News
                {
                    Title = title,
                    Link = link,
                    Description = description,
                    PublicationDate = publicationDate,
                });
            }
        }

        public IEnumerable<NewsDto> GetNews()
        {
            return _mapper.Map<IEnumerable<NewsDto>>(_newsRepository.GetAll().AsEnumerable());
        }

        public IEnumerable<NewsDto> GetNewsBySubstring(string substring, NewsFilterType filterType)
        {
            var stringComprasion = StringComparison.OrdinalIgnoreCase;
            var filteredNews = _newsRepository.GetAll().AsEnumerable();
            if (filterType == NewsFilterType.Title)
                filteredNews = filteredNews.Where(e => e.Title.Contains(substring, stringComprasion));
            if (filterType == NewsFilterType.Description)
                filteredNews = filteredNews.Where(e => e.Description.Contains(substring, stringComprasion));
            if (filterType == NewsFilterType.TitleAndDescription)
                filteredNews = filteredNews.Where(e => e.Title.Contains(substring, stringComprasion)
                    && e.Description.Contains(substring, stringComprasion));

            return _mapper.Map<IEnumerable<NewsDto>>(filteredNews);
        }
    }
}
