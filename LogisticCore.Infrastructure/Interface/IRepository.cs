using System.Linq.Expressions;

namespace LogisticCore.Infrastructure.Interface
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> query);
        TEntity GetById(int id);
        void Add(TEntity item);
        void AddRange(IEnumerable<TEntity> entities);
        int Update(TEntity item);
        bool Delete(TEntity item);
        public bool DeleteRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> ExecStoredProcedure(string sp_Name, params object[] parameter);
    }
}
