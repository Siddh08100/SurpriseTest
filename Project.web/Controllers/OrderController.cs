using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Repository.ViewModels;
using Project.Service.Interface;

namespace Project.web.Controllers;

[Authorize(Roles = "Users")]
public class OrderController : Controller
{
    private readonly IProductService _service;
    private readonly IOrderService _OrderService;
    public OrderController(IProductService service, IOrderService OrderService)
    {
        _service = service;
        _OrderService = OrderService;
    }

    public async Task<IActionResult> Index()
    {
        List<AddEditProductViewModel> addEditProductViewModel = await _service.GetAllProduct();
        OrderDetailsViewModel orderDetailsViewModel = new()
        {
            Items = addEditProductViewModel,
        };
        return View(orderDetailsViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CheckOutOrder(OrderDetailsViewModel model)
    {
        try
        {
            string Userid = User.Claims.FirstOrDefault(claims => claims.Type == ClaimTypes.NameIdentifier).Value;
            await _OrderService.AddOrderDetails(model, Userid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Json("error");
        }
        return Json("Ok");
    }
}
