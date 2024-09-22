using Microsoft.AspNetCore.Mvc.Rendering;
using MyWeb.Models;
using MyWeb.Repositories;
using MyWeb.ViewModels;

namespace MyWeb.Service;

public class SearchService(StudentRepo studentRepo, MajorRepo majorRepo, GradeRepo gradeRepo)
{
    public SearchViewModel GetList(SearchViewModel model)
    {
        List<Student> students = studentRepo.GetList(model.MajorId, model.GradeId, model.ClassId, model.Gender, model.Name, model.Number);
        foreach (var student in students)
        {
            model.Results.Add(new ResultViewModel()
            {
                MajorName = student.Class.Major.Name,
                GradeName = student.Class.Grade.Name,
                ClassName = student.Class.Name,
                Number = student.Number,
                Name = student.Name,
                Gender = student.Gender,
                Birthday = student.Birthday
            });
        }

        List<Major> majors = majorRepo.GetList();
        model.Majors = new SelectList(majors, "Id", "Name");

        List<Grade> grades = gradeRepo.GetList();
        model.Grades = new SelectList(grades, "Id", "Name");

        return model;
    }
}
