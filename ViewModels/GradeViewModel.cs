using System.ComponentModel.DataAnnotations;

namespace MyWeb.ViewModels;

public class GradeViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "年级不能为空")]
    [Display(Name = "年级")]
    public string Name { get; set; } = string.Empty;
}