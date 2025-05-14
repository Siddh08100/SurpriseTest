using Project.Repository.Data;
using Project.Repository.Interface;
using Project.Repository.ViewModels;
using Project.Service.Interface;

namespace Project.Service.Implementations;

public class LoginService : ILoginService
{
    private readonly IUserRepository _repository;

    public LoginService(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<UserViewModel> GetUserByEmail(LoginViewModel model)
    {
        User user = await _repository.GetUserByEmail(model.Email);
        if (user != null && user.Password == model.Password)
        {
            UserViewModel userViewModel = new()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.Name,
            };
            return userViewModel;
        }
        return null;
    }
}
