using MyWeb.Models;
using MyWeb.Repositories;
using MyWeb.ViewModels;

namespace MyWeb.Service;

public class GradeService(GradeRepo gradeRepo)
{
    public void Insert(GradeViewModel model)
    {
        Grade grade = new Grade()
        {
            Name = model.Name,
        };
        gradeRepo.Insert(grade);
    }

    public List<GradeViewModel> GetList()
    {
        List<Grade> grades = gradeRepo.GetList();
        List<GradeViewModel> gradeViewModels = new List<GradeViewModel>();
        foreach (var grade in grades)
        {
            gradeViewModels.Add(new GradeViewModel()
            {
                Id = grade.Id,
                Name = grade.Name,
            });
        }
        return gradeViewModels;
    }

    public GradeViewModel Get(Guid id)
    {
        Grade? grade = gradeRepo.Get(id);
        if (grade == null)
        {
            throw new NullReferenceException();
        }

        GradeViewModel model = new GradeViewModel()
        {
            Id = grade.Id,
            Name = grade.Name,
        };
        return model;
    }

    public void Update(GradeViewModel model)
    {
        Grade grade = new Grade()
        {
            Id = model.Id,
            Name = model.Name,
        };
        gradeRepo.Update(grade);
    }

    public void Delete(Guid id)
    {
        Grade? grade = gradeRepo.GetWithClasses(id);
        if (grade == null)
        {
            throw new NullReferenceException("未找到该年级，无法删除");
        }
        if (grade.ClassList.Any())
        {
            throw new InvalidOperationException("年级中存在班级，无法删除");
        }
        gradeRepo.Delete(grade);
    }

}
