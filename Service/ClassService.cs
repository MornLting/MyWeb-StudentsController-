using Microsoft.AspNetCore.Mvc.Rendering;
using MyWeb.Models;
using MyWeb.Repositories;
using MyWeb.ViewModels;

namespace MyWeb.Service;

public class ClassService(ClassRepo classRepo, MajorRepo majorRepo, GradeRepo gradeRepo)
{
    public ClassViewModel GetSelectListModel()
    {
        ClassViewModel model = new ClassViewModel();

        List<Major> majors = majorRepo.GetList();
        model.Majors = new SelectList(majors, "Id", "Name");

        List<Grade> grades = gradeRepo.GetList();
        model.Grades = new SelectList(grades, "Id", "Name");

        return model;
    }
    public void Insert(ClassViewModel model)
    {
        Class @class = new Class()
        {
            GradeId = model.GradeId,
            MajorId = model.MajorId,
            Name = model.Name,
        };
        classRepo.Insert(@class);
    }
    public List<ClassViewModel> GetList()
    {
        List<Class> classes = classRepo.GetListWithMajorAndGrade();
        List<ClassViewModel> classViewModels = new List<ClassViewModel>();
        foreach (var @class in classes)
        {
            classViewModels.Add(new ClassViewModel()
            {
                Id = @class.Id,
                Name = @class.Name,
                MajorName = @class.Major.Name,
                GradeName = @class.Grade.Name,
            });
        }
        return classViewModels;
    }

    public ClassViewModel GetEditModel(Guid id)
    {
        Class? @class = classRepo.Get(id);
        if (@class == null)
        {
            throw new NullReferenceException();
        }

        ClassViewModel model = new ClassViewModel()
        {
            Id = @class.Id,
            MajorId = @class.MajorId,
            GradeId = @class.GradeId,
            Name = @class.Name,
        };

        ClassViewModel selectListModel = GetSelectListModel();
        model.Majors = selectListModel.Majors;
        model.Grades = selectListModel.Grades;

        return model;
    }

    public void Update(ClassViewModel model)
    {
        Class @class = new Class
        {
            Id = model.Id,
            MajorId = model.MajorId,
            GradeId = model.GradeId,
            Name = model.Name
        };
        classRepo.Update(@class);
    }

    public void Delete(Guid id)
    {
        Class? @class = classRepo.GetWithStudents(id);
        if (@class == null)
        {
            throw new NullReferenceException("未找到该班级，无法删除");
        }
        if (@class.Students.Any())
        {
            throw new InvalidOperationException("班级中存在学生，无法删除");
        }
        classRepo.Delete(@class);
    }
}