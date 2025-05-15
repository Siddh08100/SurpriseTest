using Project.Repository.ViewModels;

namespace Project.Service.Interface;

public interface IOrderService
{
    Task AddOrderDetails(OrderDetailsViewModel model, string userId);
    Task<bool> CheckStockStatus(int Id, int quantity);

    Task UpdateOrderStatus(int id, string status,string userId);
    Task<List<OrderDetailsViewModel>> GetAllOrders();
}
