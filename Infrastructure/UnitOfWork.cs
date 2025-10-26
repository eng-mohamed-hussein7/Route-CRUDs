using Application.Interfaces;
using Application.IRepositories;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);

        if (!_repositories.TryGetValue(type, out var repo))
        {
            repo = new Repository<T>(_context);
            _repositories[type] = repo;
        }

        return (IRepository<T>)repo;
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    public int SaveChanges() => _context.SaveChanges();

    public void Dispose() => _context.Dispose();
}
