using NewsAggregator.DataAccess.Data;
using NewsAggregator.DataAccess.Entities;

namespace NewsAggregator.DataAccess.Repositories
{
    public class NewsRepository : BaseRepository<News>
    {
        public NewsRepository(NewsAggregatorContext context) 
            : base(context)
        {
        }
    }
}
