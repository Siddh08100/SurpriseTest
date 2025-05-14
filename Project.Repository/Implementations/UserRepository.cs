using Microsoft.EntityFrameworkCore;
using Project.Repository.Data;
using Project.Repository.Interface;

namespace Project.Repository.Implementations;

public class UserRepository : IUserRepository
{
    private readonly ProjectContext _context;

    public UserRepository(ProjectContext context)
    {
        _context = context;
    }
    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.User.Where(s => s.Email.ToLower() == email.Trim().ToLower()).Include(s => s.Role).FirstOrDefaultAsync(); 
    }
}
