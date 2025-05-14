using Project.Repository.ViewModels;

namespace Project.Service.Interface;

public interface ILoginService
{
    Task<UserViewModel> GetUserByEmail(LoginViewModel model);
}
