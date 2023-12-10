using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WillsPlatform.Application.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WillsPlatform.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public void Delete(object id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
        }

        public T Get(object id)
        {
            var x = _dbSet.Find(id);
            return x;
        }

        public async Task<T?> GetAsync(object id)
        {
            var x = await _dbSet.FindAsync(id);
            return x;
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var obj = await _dbSet.Where(predicate).FirstOrDefaultAsync();
            return obj;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate == null)
                return await Task.FromResult((IEnumerable<T>)_dbSet.AsNoTracking().AsAsyncEnumerable());

            return await Task.FromResult((IEnumerable<T>)_dbSet.AsNoTracking().Where(predicate).AsAsyncEnumerable());
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbSet.AsNoTracking().AsQueryable();

            if (predicate != null)
               query = _dbSet.Where(predicate).AsNoTracking().AsQueryable();

            if (includes != null)
                return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            else
                return query;
        }

        public Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task CountAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
