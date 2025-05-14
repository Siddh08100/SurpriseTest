using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Interface;
using Project.web.Models;

namespace Project.web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILoginService _service;
    private readonly IJwtService _jwtService;

    public HomeController(ILogger<HomeController> logger, ILoginService service, IJwtService jwtService)
    {
        _logger = logger;
        _service = service;
        _jwtService = jwtService;
    }


    public IActionResult Index()
    {
        var authToken = Request.Cookies["AuthToken"];
        var (role, email) = _jwtService.ValidateToken(authToken);
        if (role == "Users")
        {
            return RedirectToAction("UserIndex", "Home");
        }
        return View();
    }

    [Authorize(Roles = "Users")]
    public IActionResult UserIndex()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return RedirectToAction("Login", "Login");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
