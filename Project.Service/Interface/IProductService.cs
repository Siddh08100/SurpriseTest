using Project.Repository.ViewModels;

namespace Project.Service.Interface;

public interface IProductService
{
    Task<AddEditProductViewModel> GetProductDetails(int id);
    Task<List<AddEditProductViewModel>> GetAllProduct();
    Task AddEditProduct(AddEditProductViewModel product,string userId);
    Task<bool> IsProductExist(int id, string name,string category);

    Task DeleteProduct(int id);
}
