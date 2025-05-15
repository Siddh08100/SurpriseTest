using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Repository.ViewModels;
using Project.Service.Interface;

namespace Project.web.Controllers;

[Authorize(Roles = "Admin")]
public class ProductsController : Controller
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }
    public async Task<IActionResult> Index()
    {
        List<AddEditProductViewModel> addEditProductViewModel = await _service.GetAllProduct();
        return View(addEditProductViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AddEditModal(int id)
    {
        if (id == 0)
        {
            return PartialView("_AddEditProduct", new AddEditProductViewModel());
        }
        else
        {
            try
            {
                AddEditProductViewModel addEditProductViewModel = await _service.GetProductDetails(id);
                return PartialView("_AddEditProduct", addEditProductViewModel);
            }
            catch (Exception)
            {
                return Json("error");
            }
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        List<AddEditProductViewModel> addEditProductViewModel = await _service.GetAllProduct();
        return PartialView("_DisplayProducts", addEditProductViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddEditProduct(AddEditProductViewModel model)
    {
        bool checkIsExist = await _service.IsProductExist(model.Id, model.Name, model.Category);
        if (!checkIsExist)
        {
            return Json("exist");
        }
        else
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(claims => claims.Type == ClaimTypes.NameIdentifier).Value;
                await _service.AddEditProduct(model, userId);
            }
            catch (Exception)
            {
                return Json("Error");
            }
        }
        if (model.Id == 0)
        {
            return Json("OK");
        }
        else
        {
            return Json("Edit");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            await _service.DeleteProduct(id);
        }
        catch (Exception)
        {
            return Json("error");
        }
        return Json("ok");
    }
}
