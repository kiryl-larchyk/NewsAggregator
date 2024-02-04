using Microsoft.EntityFrameworkCore;
using NewsAggregator.DataAccess.Data;
using NewsAggregator.DataAccess.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly NewsAggregatorContext _context;

        public BaseRepository(NewsAggregatorContext context)
        {
            _context = context;
        }

        public async Task<int> Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(int id)
        {
            var entity = await Get(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public async Task Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
