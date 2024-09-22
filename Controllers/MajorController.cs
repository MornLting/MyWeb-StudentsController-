using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Data;
using MyWeb.Models;
using MyWeb.Repositories;
using MyWeb.Service;
using MyWeb.ViewModels;

namespace MyWeb.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class MajorController(MajorService majorService) : Controller
    {
        public IActionResult Index()
        {
            return View(majorService.GetList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(MajorViewModel model)
        {
            if (ModelState.IsValid)
            {
                majorService.Insert(model);
                return RedirectToAction("Index");
            }
           return View(model);
        }

        public IActionResult Edit(Guid id)
        {
            MajorViewModel model;
            try
            {
                model = majorService.Get(id);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MajorViewModel model)
        {
            if (ModelState.IsValid)
            {
                majorService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                majorService.Delete(id);
                return Ok("删除成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
