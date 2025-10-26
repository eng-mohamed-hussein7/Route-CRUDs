using Application.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> Entities;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        Entities = _context.Set<T>();
    }

    // إضافة كيان واحد أو مجموعة كيانات
    public async Task<T> AddAsync(T entity)
    {
        await Entities.AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await Entities.AddRangeAsync(entities);
        return entities;
    }

    public Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
    {
        return predicate == null
            ? Entities.CountAsync()
            : Entities.CountAsync(predicate);
    }

    public void Delete(T entity) => Entities.Remove(entity);
    public void DeleteRange(IEnumerable<T> entities) => Entities.RemoveRange(entities);

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        return await ApplyIncludes(includes)
            .ToListAsync();
    }

    public async Task<T> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate = null,
        params Expression<Func<T, object>>[] includes)
    {
        var query = ApplyIncludes(includes);
        return predicate == null
            ? await query.FirstOrDefaultAsync()
            : await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<T> SingleOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        return await ApplyIncludes(includes)
            .SingleOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> WhereAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        return await ApplyIncludes(includes)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(params object[] keyValues)
    {
        return await Entities.FindAsync(keyValues);
    }

    public void Update(T entity) => Entities.Update(entity);

    private IQueryable<T> ApplyIncludes(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Entities.AsNoTracking();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return query;
    }
}
