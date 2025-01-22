namespace MVC.Core.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(int userId, string username);
}