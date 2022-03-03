using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext _context;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public bool Commit()
        {
            try
            {
                _context.Database.CommitTransaction();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public IRepository<TEntity> Repository<TEntity>()
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, dynamic>();

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
                return (IRepository<TEntity>)_repositories[type];

            var repositoryType = typeof(Repository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), this));

            return _repositories[type];
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
