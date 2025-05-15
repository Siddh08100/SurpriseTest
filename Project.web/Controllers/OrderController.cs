using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.web.Controllers;

[Authorize(Roles = "Users")]
public class OrderController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}
