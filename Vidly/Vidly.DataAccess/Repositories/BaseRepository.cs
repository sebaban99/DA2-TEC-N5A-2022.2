using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vidly.DataAccess.Contexts;
using Vidly.IDataAccess;

namespace Vidly.DataAccess;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly VidlyContext _context;

    public BaseRepository(VidlyContext context)
    {
        _context = context;
    }
    
    // Para cambiar el estado de un elemento:
    // _context.Entry(elem).State = EntityState.Unchanged;

    public virtual IEnumerable<T> GetAllBy(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public T? GetOneBy(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().FirstOrDefault(expression);
    }

    public void Insert(T elem)
    {
        _context.Set<T>().Add(elem);
    }

    public void Delete(T elem)
    {
        _context.Set<T>().Remove(elem);
    }

    public void Update(T elem)
    {
        _context.Set<T>().Update(elem);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}