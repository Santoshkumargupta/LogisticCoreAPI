using LogisticCore.Domain.Model;
using LogisticCore.Infrastructure.Context;
using LogisticCore.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LogisticCore.Infrastructure.DbAccess
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : IDEntity
    {
        private readonly LogisticCoreContext _dbContext;
        public GenericRepository(LogisticCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var result = _dbContext.Set<TEntity>().OrderBy(x => x.Id).ToList();
            return result;
        }
        public TEntity GetById(int id)
        {
            var result = _dbContext.Set<TEntity>().Find(id);

            //_dbContext.Entry(result).State = EntityState.Detached;
            return result;
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> query)
        {
            var result = _dbContext.Set<TEntity>().Where(query);
            return result;
        }

        public void Add(TEntity item)
        {
            _dbContext.Set<TEntity>().Add(item);

        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }
        public int Update(TEntity item)
        {
            _dbContext.Set<TEntity>().Update(item);
            var result = _dbContext.SaveChanges();
            return result;
        }
        public bool Delete(TEntity item)
        {
            _dbContext.Remove(item);
            var result = _dbContext.SaveChanges();
            return result > 0 ? true : false;
        }

        public bool DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
            var result = _dbContext.SaveChanges();
            return result > 0 ? true : false;
        }



        public IEnumerable<TEntity> ExecStoredProcedure(string sp_Name, params object[] parameter)
        {

            var result = _dbContext.Set<TEntity>().FromSqlRaw(sp_Name, parameter).ToList();
            return result;
        }

    }
}
