using Microsoft.AspNetCore.Mvc;
using MVC.Core.Interfaces.Services;
using MVC.Presentation.Models;

namespace MVC.Presentation.Controllers;

public class AuthController(IUserService userService) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }

        var result = await userService.Login(model.Username, model.Password);

        HttpContext.Response.Cookies.Append("AuthToken", result.Value.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
        
        return RedirectToAction("Master", "Cutting");
    }
}