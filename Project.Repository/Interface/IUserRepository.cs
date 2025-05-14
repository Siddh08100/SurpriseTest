using Project.Repository.Data;

namespace Project.Repository.Interface;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
}
