using MVC.Core.Dtos;

namespace MVC.Core.Interfaces.Services;

public interface IUserService
{
    Task<Result<LoginDto>> Login(string username, string password);
}