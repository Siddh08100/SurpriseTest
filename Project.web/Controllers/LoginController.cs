using Microsoft.AspNetCore.Mvc;
using Project.Repository.ViewModels;
using Project.Service.Interface;

namespace Project.web.Controllers;

public class LoginController : Controller
{
    private readonly ILoginService _service;
    private readonly IJwtService _jwtService;

    public LoginController(ILoginService service, IJwtService jwtService)
    {
        _service = service;
        _jwtService = jwtService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        var authToken = Request.Cookies["AuthToken"];
        var (role, email) = _jwtService.ValidateToken(authToken);
        if (role != null && email != null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                UserViewModel userViewModel = await _service.GetUserByEmail(model);
                if (userViewModel != null)
                {
                    string token = _jwtService.GenerateJwtToken(model.Email, userViewModel.Role, userViewModel.Id);
                    Response.Cookies.Append("AuthToken", token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = "Invalid login attempt";
                }
            }
            catch (Exception)
            {
                TempData["error"] = "error Occured";
            }
        }
        return View(model);
    }
}
