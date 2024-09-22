using Microsoft.EntityFrameworkCore;
using MyWeb.Data;
using MyWeb.Models;
using System.Collections.Generic;

namespace MyWeb.Repositories;

public class ClassRepo(AppDbContext context) : BaseRepositiry<Class>(context)
{
    public List<Class> GetListWithMajorAndGrade()
    {
        return dbSet.Include(c => c.Major).Include(c => c.Grade).ToList();
    }

    public List<Class> GetList(Guid? majorId, Guid? gradeId)
    {
        IQueryable<Class> query = dbSet.AsQueryable();
        if (majorId != null)
        {
            query = query.Where(c => c.MajorId.Equals(majorId));
        }
        if (gradeId != null)
        {
            query = query.Where(c => c.GradeId.Equals(gradeId));
        }
        return query.ToList();
    }
    public Class? GetWithStudents(Guid id)
    {
        return dbSet.Include(c => c.Students).SingleOrDefault(c => c.Id == id);
    }
}
