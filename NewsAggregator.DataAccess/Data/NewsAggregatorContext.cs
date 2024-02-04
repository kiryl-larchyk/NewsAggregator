using Microsoft.EntityFrameworkCore;
using NewsAggregator.DataAccess.Entities;

namespace NewsAggregator.DataAccess.Data
{
    public class NewsAggregatorContext : DbContext
    {
        public NewsAggregatorContext(DbContextOptions<NewsAggregatorContext> options)
            : base(options) 
        {
        }

        public DbSet<News> News { get; set; }
    }
}
