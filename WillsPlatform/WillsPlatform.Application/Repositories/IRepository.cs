using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WillsPlatform.Application.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[]? includes);
        T Get(object id);
        Task<T?> GetAsync(object id);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void Add(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(object id);
        Task DeleteAsync(T entity);
        int Count();
        Task CountAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}