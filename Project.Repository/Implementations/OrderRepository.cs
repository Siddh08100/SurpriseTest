using Microsoft.EntityFrameworkCore;
using Project.Repository.Data;
using Project.Repository.Interface;
using Project.Repository.ViewModels;

namespace Project.Repository.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly ProjectContext _context;

    public OrderRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<int> AddOrder(Orders orders)
    {
        await _context.Orders.AddAsync(orders);
        await _context.SaveChangesAsync();
        return orders.Id;
    }

    public async Task AddProductOrders(ProductOrder productOrder)
    {
        await _context.ProductOrders.AddAsync(productOrder);
        await _context.SaveChangesAsync();
    }

    public async Task<List<OrderDetailsViewModel>> GetAllOrders()
    {
        return await _context.Orders.Select(s => new OrderDetailsViewModel
        {
            TotalAmount = (int?)s.Totalamount,
            ProductNames = s.Productitems.Select(t => t.Name).ToList(),
            OrderId = s.Id
            // Status = s.Status
        }).ToListAsync();
    }
}
