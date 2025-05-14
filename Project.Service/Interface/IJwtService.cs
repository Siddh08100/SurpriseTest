namespace Project.Service.Interface;

public interface IJwtService
{
    string GenerateJwtToken(string email, string role, int id);

    (string, string) ValidateToken(string token);
}
