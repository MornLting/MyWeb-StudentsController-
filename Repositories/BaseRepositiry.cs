using Microsoft.EntityFrameworkCore;
using MyWeb.Data;

namespace MyWeb.Repositories;

public class BaseRepositiry<T>(AppDbContext context) where T : class, new()
{
    protected DbSet<T> dbSet => context.Set<T>();

    public void Insert(T entity)
    { 
        dbSet.Add(entity);
        context.SaveChanges();//保存到数据库
    }

    public void Delete(T entity)
    { 
        dbSet.Remove(entity); 
        context.SaveChanges();
    }

    public void Update(T entity)
    { 
        dbSet.Update(entity);
        context.SaveChanges();
    }
    public T? Get(Guid id)
    { 
        return dbSet.Find(id);
    }
    public List<T> GetList()
    { 
        return dbSet.ToList();
    }
}
