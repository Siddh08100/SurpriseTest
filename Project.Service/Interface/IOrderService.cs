using Project.Repository.ViewModels;

namespace Project.Service.Interface;

public interface IOrderService
{
    Task AddOrderDetails(OrderDetailsViewModel model, string userId);

    Task<List<OrderDetailsViewModel>> GetAllOrders();
}
