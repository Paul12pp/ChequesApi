using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.Repositories
{
    public interface IUnitOfWork
    {
        public DbContext Context { get; }
        void BeginTransaction();
        void SaveChanges();
        bool Commit();
        void Rollback();
        IRepository<TEntity> Repository<TEntity>();
    }
}
