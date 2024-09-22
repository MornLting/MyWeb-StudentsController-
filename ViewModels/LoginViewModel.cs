using System.ComponentModel.DataAnnotations;

namespace MyWeb.ViewModels;

public class LoginViewModel
{
    [Display(Name = "用户名")]
    [Required(ErrorMessage = "用户名不能为空")]
    public string Username { get; set; } = string.Empty;

    [Display(Name = "密码")]
    [Required(ErrorMessage = "密码不能为空")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
