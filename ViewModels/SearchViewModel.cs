using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyWeb.ViewModels;

public class SearchViewModel
{
    public Guid? MajorId { get; set; }
    public Guid? GradeId { get; set; }
    public Guid? ClassId { get; set; }
    public string? Number { get; set; }
    public string? Name { get; set; }
    public bool? Gender { get; set; }

    public SelectList? Majors { get; set; }
    public SelectList? Grades { get; set; }

    public List<ResultViewModel> Results { get; set; } = new List<ResultViewModel>();
}