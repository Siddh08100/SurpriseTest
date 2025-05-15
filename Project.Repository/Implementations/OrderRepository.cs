using Project.Repository.Data;
using Project.Repository.Interface;

namespace Project.Repository.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly ProjectContext _context;

    public OrderRepository(ProjectContext context)
    {
        _context = context;
    }
}
