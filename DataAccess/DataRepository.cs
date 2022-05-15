using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Models.Concerete;

namespace DataAccess;

//GENERIC REPOSITORY IMPLEMENTATION
public interface IRepository<T> where T : class
{
    T Get(Guid id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
        Context = context;
    }
    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }
    public void AddRange(IEnumerable<T> entities)
    {
        Context.Set<T>().AddRange(entities);
    }
    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return Context.Set<T>().Where(predicate);
    }
    public T Get(Guid id)
    {
        return Context.Set<T>().Find(id);
    }
    public IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }
    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }
    public void RemoveRange(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
    }
}

public interface IUrlRepository : IRepository<UrlShorteningModel>
{
}

public class UrlRepository : Repository<UrlShorteningModel>, IUrlRepository
{
    public UrlShortenerContext UrlDbContext
    {
        get { return Context as UrlShortenerContext; }
    }
    public UrlRepository(UrlShortenerContext context) : base(context)
    {
    }
}

// UoW IMPLEMENTATION
public interface IUnitOfWork : IDisposable
{
    UrlRepository UrlShortenings { get; }
    int Complete();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly UrlShortenerContext _context;
    public UrlRepository UrlShortenings { get; private set; }
    public UnitOfWork(UrlShortenerContext context)
    {
        _context = context;
        UrlShortenings = new UrlRepository(_context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}