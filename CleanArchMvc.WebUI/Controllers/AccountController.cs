using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;

namespace CleanArchMvc.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly IAutheticate _authentication;

    public AccountController(IAutheticate authentication)
    {
        _authentication = authentication;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _authentication.Authenticate(
            model.Email,
            model.Password
        );

        if (result)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(model.ReturnUrl);
        }

        ModelState.AddModelError(
            string.Empty, "Invalid login attempt. (password must be strong)"
        );
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var result = await _authentication.RegisterUser(
            model.Email,
            model.Password
        );

        if (!result)
        {
            ModelState.AddModelError(
                string.Empty,
                "Invalid register attempt (password must be strong.)"
            );
            return View(model);
        }

        return Redirect("/");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authentication.Logout();
        return Redirect("/Account/Login");
    }
}
