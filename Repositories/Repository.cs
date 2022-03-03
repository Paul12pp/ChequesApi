using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbContext = _unitOfWork.Context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }
        public IEnumerable<TEntity> FindAll()
        {
            return _dbContext.Set<TEntity>();
        }
        public void Insert(TEntity entity)
        {

            _dbSet.Add(entity);
            _unitOfWork.SaveChanges();

        }
        public void Update(TEntity entity)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

            _unitOfWork.Commit();

        }
        public void Delete(TEntity entity)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                _dbSet.Remove(entity);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

            _unitOfWork.Commit();


        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }


        public IRepository<TEntity> GetRepository()
        {
            return _unitOfWork.Repository<TEntity>();
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                _dbSet.AddRange(entities);

            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                _dbSet.UpdateRange(entities);

            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

        }
    }
}
