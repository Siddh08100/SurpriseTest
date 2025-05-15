using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.Repository.Data;
using Project.Repository.Interface;
using Project.Repository.ViewModels;
using Project.Service.Interface;

namespace Project.Service.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<AddEditProductViewModel> GetProductDetails(int id)
    {
        Products product = await _repository.GetProductById(id);
        AddEditProductViewModel addEditProductViewModel = new()
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
        return addEditProductViewModel;
    }

    public async Task<List<AddEditProductViewModel>> GetAllProduct()
    {
        return await _repository.GetAllProducts();
    }

    public async Task<bool> IsProductExist(int id, string name, string category)
    {
        return await _repository.IsProductExist(id, name, category);
    }

    public async Task AddEditProduct(AddEditProductViewModel product, string userId)
    {
        if (product.Id == 0)
        {
            Products products = new()
            {
                Name = product.Name,
                Category = product.Category,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Createddate = DateTime.Now,
                Updateddate = DateTime.Now,
                Createdby = userId,
                Updatedby = userId,
            };
            await _repository.AddProduct(products);
        }
        else
        {
            Products products = await _repository.GetProductById(product.Id);
            products.Name = product.Name;
            products.Description = product.Description;
            products.Category = product.Category;
            products.Price = product.Price;
            products.Stock = product.Stock;
            products.Updateddate = DateTime.Now;
            products.Updatedby = userId;
            await _repository.UpdateProduct(products);
        }
    }

    public async Task DeleteProduct(int id)
    {
        await _repository.DeleteProduct(id);
    }
}
