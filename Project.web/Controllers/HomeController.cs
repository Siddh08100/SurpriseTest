using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Repository.ViewModels;
using Project.Service.Interface;
using Project.web.Models;

namespace Project.web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILoginService _service;
    private readonly IOrderService _OrderService;
    private readonly IJwtService _jwtService;

    public HomeController(ILogger<HomeController> logger, ILoginService service, IJwtService jwtService, IOrderService OrderService)
    {
        _logger = logger;
        _service = service;
        _jwtService = jwtService;
        _OrderService = OrderService;
    }

    public async Task<IActionResult> Index()
    {
        var authToken = Request.Cookies["AuthToken"];
        var (role, email) = _jwtService.ValidateToken(authToken);
        if (role == "Users")
        {
            return RedirectToAction("UserIndex", "Home");
        }
        List<OrderDetailsViewModel> orderDetailsViewModels = await _OrderService.GetAllOrders();
        return View(orderDetailsViewModels);
    }

    [Authorize(Roles = "Users")]
    public async Task<IActionResult> UserIndex()
    {
        List<OrderDetailsViewModel> orderDetailsViewModels = await _OrderService.GetAllOrders();
        return View(orderDetailsViewModels);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return RedirectToAction("Login", "Login");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult ForbiddenPage()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult PageNotFound()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        List<OrderDetailsViewModel> orderDetailsViewModels = await _OrderService.GetAllOrders();
        return PartialView("_OrderDetails", orderDetailsViewModels);
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
