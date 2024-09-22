using MyWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace MyWeb.ViewModels
{
    public class MajorViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "专业名称不能为空")]
        [Display(Name="专业名称")]
        public string Name { get; set; } = string.Empty;

    }
}
