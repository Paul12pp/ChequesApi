using System.Collections.Generic;
using System.Linq;

namespace ChequesApi.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity Find(params object[] keyValues);
        IEnumerable<TEntity> FindAll();
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        IQueryable<TEntity> Queryable();

        IRepository<TEntity> GetRepository();

    }
}
