using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Data;
using MyWeb.Service;
using MyWeb.ViewModels;

namespace MyWeb.Controllers;

//[Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Teacher)}")]
[Authorize]
public class HomeController(SearchService searchService) : Controller
{
    public IActionResult Index(SearchViewModel model)
    {
        return View(searchService.GetList(model));
    }

    [Route("error/404")]
    public IActionResult Error404()
    {
        return View();
    }

}
