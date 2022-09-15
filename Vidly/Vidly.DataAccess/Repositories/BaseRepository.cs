using System.Linq.Expressions;
using Vidly.Domain.Entities;
using Vidly.IDataAccess;

namespace Vidly.DataAccess;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly VidlyContext _context;

    public BaseRepository(VidlyContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAllByExpression(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public T GetOneByExpression(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().FirstOrDefault(expression);
    }

    public void InsertOne(T elem)
    {
        _context.Set<T>().Add(elem);
    }

    public void DeleteOne(T elem)
    {
        _context.Set<T>().Remove(elem);
    }

    public void UpdateOne(T elem)
    {
        _context.Set<T>().Update(elem);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}