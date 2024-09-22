using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Data;
using MyWeb.Models;
using MyWeb.Repositories;
using MyWeb.Service;
using MyWeb.ViewModels;

namespace MyWeb.Controllers;
[Authorize(Roles = nameof(UserRole.Admin))]
public class GradeController(GradeService gradeService) : Controller
{
    public IActionResult Index()
    {
        return View(gradeService.GetList());
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(GradeViewModel model)
    {
        if (ModelState.IsValid)
        {
            gradeService.Insert(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Edit(Guid id)
    {
        GradeViewModel model;
        try
        {
            model = gradeService.Get(id);
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(GradeViewModel model)
    {
        if (ModelState.IsValid)
        {
            gradeService.Update(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }
    public IActionResult Delete(Guid id)
    {
        try
        {
            gradeService.Delete(id);
            return Ok("删除成功");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
