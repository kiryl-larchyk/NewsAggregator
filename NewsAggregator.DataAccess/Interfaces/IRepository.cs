using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.DataAccess.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> Get(int id);

        Task<int> Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(int id);
    }
}
