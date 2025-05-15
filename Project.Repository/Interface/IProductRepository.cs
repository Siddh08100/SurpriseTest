using Project.Repository.Data;
using Project.Repository.ViewModels;

namespace Project.Repository.Interface;

public interface IProductRepository
{
    Task<List<AddEditProductViewModel>> GetAllProducts();
    Task<Products> GetProductById(int id);
    Task AddProduct(Products product);
    Task UpdateProduct(Products product);
    Task DeleteProduct(int id);

    Task<bool> IsProductExist(int id, string name,string category);
}
