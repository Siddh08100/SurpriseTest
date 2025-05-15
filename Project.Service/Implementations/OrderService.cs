using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Project.Repository.Data;
using Project.Repository.Interface;
using Project.Repository.ViewModels;
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


    public async Task<bool> CheckStockStatus(int Id, int quantity)
    {
        Products products = await _repository.GetProductById(Id);
        return (products.Stock - quantity) >= 0;
    }

    public async Task AddOrderDetails(OrderDetailsViewModel model, string userId)
    {
        using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            Orders orders = new()
            {
                Totalamount = (decimal)(model.TotalAmount ?? 0),
                Createdby = userId,
                Createddate = DateTime.Now,
                Updatedby = userId,
                Status = model.Status ?? " ",
                Updateddate = DateTime.Now
            };
            int orderId = await _OrderRepository.AddOrder(orders);

            if (model.Items.Count() > 0)
            {
                foreach (AddEditProductViewModel items in model.Items.ToList())
                {
                    Products products = await _repository.GetProductById(items.Id);
                    products.Stock = (short)(products.Stock - items.Stock);
                    ProductOrder productOrders = new()
                    {
                        Productsid = products.Id,
                        Name = products.Name,
                        Quantity = items.Stock,
                        Rate = items.Price,
                        Amount = (decimal)(items.Stock * items.Price),
                        Orderid = orderId,
                    };
                    await _repository.UpdateProduct(products);
                    await _OrderRepository.AddProductOrders(productOrders);
                }
            }
            scope.Complete();
        }
        catch (Exception)
        {
            scope.Dispose();
        }
    }

    public async Task<List<OrderDetailsViewModel>> GetAllOrders()
    {
        return await _OrderRepository.GetAllOrders();
    }

    public async Task UpdateOrderStatus(int id, string status, string userId)
    {
        Orders orders = await _OrderRepository.GetOrderById(id);
        orders.Status = status;
        orders.Updateddate = DateTime.Now;
        orders.Updatedby = userId;
        await _OrderRepository.UpdateOrder(orders);
    }
}
