using System.Linq.Expressions;

namespace Application.IRepositories;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    Task<T> GetByIdAsync(params object[] keyValues);
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

    Task<T> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate = null,
        params Expression<Func<T, object>>[] includes);

    Task<T> SingleOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);

    Task<IEnumerable<T>> WhereAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);

    Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
}