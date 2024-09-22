using Microsoft.EntityFrameworkCore;
using MyWeb.Data;
using MyWeb.Models;

namespace MyWeb.Repositories;

public class GradeRepo(AppDbContext context) : BaseRepositiry<Grade>(context)
{
    public Grade? GetWithClasses(Guid id)
    {
        return dbSet.Include(g => g.ClassList).SingleOrDefault(g => g.Id == id);
    }
}
