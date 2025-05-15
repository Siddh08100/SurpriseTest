using Microsoft.EntityFrameworkCore;
using Project.Repository.Data;
using Project.Repository.Interface;
using Project.Repository.ViewModels;

namespace Project.Repository.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly ProjectContext _context;

    public ProductRepository(ProjectContext context)
    {
        _context = context;
    }
    public async Task<List<AddEditProductViewModel>> GetAllProducts()
    {
        return await _context.Products.Select(s => new AddEditProductViewModel
        {
            Id = s.Id,
            Name = s.Name,
            Category = s.Category,
            Description = s.Description,
            Price = s.Price,
            Stock = s.Stock
        }).OrderBy(s => s.Id).ToListAsync();
    }

    public async Task<Products> GetProductById(int id)
    {
        return await _context.Products.Where(s => s.Id == id).FirstOrDefaultAsync();
    }
    public async Task AddProduct(Products product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateProduct(Products product)
    {
        await _context.SaveChangesAsync();
    }


    public async Task DeleteProduct(int id)
    {
        await _context.Products.Where(s => s.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> IsProductExist(int id, string name, string category)
    {
        return await _context.Products.Where(s => (s.Id == id || id == 0) && s.Name.Trim().ToLower() == name.Trim().ToLower() && s.Category.ToLower() == category.ToLower().Trim())
        .CountAsync() <= 0;
    }
}
