using Project.Repository.Interface;
using Project.Service.Interface;

namespace Project.Service.Implementations;

public class OrderService : IOrderService
{
    private readonly IProductRepository _repository;
    private readonly IOrderRepository _OrderRepository;
    public OrderService(IProductRepository repository, IOrderRepository OrderRepository)
    {
        _repository = repository;
        _OrderRepository = OrderRepository;
    }
}
