using Microsoft.EntityFrameworkCore;
using MyWeb.Data;
using MyWeb.Models;

namespace MyWeb.Repositories;

public class MajorRepo(AppDbContext context):BaseRepositiry<Major>(context)
{
    public Major? GetWithClasses(Guid id)
    {
        return dbSet.Include(m => m.ClassList).SingleOrDefault(m => m.Id == id);
    }
}
