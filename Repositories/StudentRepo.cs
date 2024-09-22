using Microsoft.EntityFrameworkCore;
using MyWeb.Data;
using MyWeb.Models;
using System.Collections.Generic;

namespace MyWeb.Repositories;

public class StudentRepo(AppDbContext context) : BaseRepositiry<Student>(context)
{
    public List<Student> GetListWithMajorAndGradeAndClass()
    {
        return dbSet
            .Include(s => s.Class).ThenInclude(c => c.Major)
            .Include(s => s.Class).ThenInclude(c => c.Grade)
            .ToList();
    }
    public Student? GetWithClass(Guid id)
    {
        return dbSet.Include(s => s.Class).SingleOrDefault(s => s.Id == id);
    }

    public List<Student> GetList(Guid? majorId, Guid? gradeId, Guid? classId, bool? gender, string? name, string? number)
    {
        IQueryable<Student> query = dbSet.AsQueryable();
        query = query.Include(s => s.Class).ThenInclude(c => c.Major).Include(s => s.Class).ThenInclude(c => c.Grade);
        if (majorId != null)
        {
            query = query.Where(s => s.Class.MajorId.Equals(majorId));
        }
        if (gradeId != null)
        {
            query = query.Where(s => s.Class.GradeId.Equals(gradeId));
        }
        if (classId != null)
        {
            query = query.Where(s => s.ClassId.Equals(classId));
        }
        if (gender != null)
        {
            query = query.Where(s => s.Gender.Equals(gender));
        }
        if (name != null)
        {
            query = query.Where(s => s.Name.Contains(name));
        }
        if (number != null)
        {
            query = query.Where(s => s.Number.Equals(number));
        }
        return query.ToList();
    }
}
