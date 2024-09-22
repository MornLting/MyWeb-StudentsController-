using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWeb.Models;

namespace MyWeb.Data;
//实体数据
public class AppDbContext:IdentityDbContext
{
    public DbSet<Major> Majors { get; set; }
    public DbSet<Grade> Grade { get; set; }
    public DbSet<Class> Class { get; set; }
    public DbSet<Student> Student { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Student>().HasIndex(s => s.Number).IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("FileName=Data/App.db");
    }
}
