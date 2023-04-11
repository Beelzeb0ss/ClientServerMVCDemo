using ClientServerMVCDemo.Data.Utility;
using System.Linq.Expressions;

namespace ClientServerMVCDemo.Data.Repos
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByID(object id);
        Task<PaginatedList<TEntity>> GetPage(int? pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, dynamic>> orderBy = null, bool isDecending = false, string includeProperties = "");
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}