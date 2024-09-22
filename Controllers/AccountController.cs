using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWeb.ViewModels;

namespace MyWeb.Controllers;

public class AccountController(SignInManager<IdentityUser> signInManager) : Controller
{
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
        }
        ModelState.AddModelError(string.Empty, "密码或用户名不正确");
        return View(model);
    }

    public IActionResult Logout()
    {
        signInManager.SignOutAsync().Wait();
        return RedirectToAction("Login");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
