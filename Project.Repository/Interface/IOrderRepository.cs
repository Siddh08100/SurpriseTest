using Project.Repository.Data;
using Project.Repository.ViewModels;

namespace Project.Repository.Interface;

public interface IOrderRepository
{
    Task<int> AddOrder(Orders orders);

    Task AddProductOrders(ProductOrder productOrder);

    Task<List<OrderDetailsViewModel>> GetAllOrders();

    Task<Orders> GetOrderById(int id);

    Task UpdateOrder(Orders order);
}
