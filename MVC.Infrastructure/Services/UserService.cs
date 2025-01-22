using MVC.Core;
using MVC.Core.Dtos;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Services;

namespace MVC.Infrastructure.Services;

public class UserService(IJwtService jwtService, IUnitOfWork unitOfWork) : IUserService
{
    public async Task<Result<LoginDto>> Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return Result<LoginDto>.Failure("Invalid username or password.");

        var user = await unitOfWork.UserRepository.GetAsync(u => u.Name == username);
        if (user == null)
            return Result<LoginDto>.Failure("Invalid username or password.");

        if (!password.VerifyPassword(user.Salt, user.HashedPassword))
            return Result<LoginDto>.Failure("Invalid username or password.");

        var token = jwtService.GenerateToken(user.Id, user.Name);
        
        return Result<LoginDto>.Success(new LoginDto
        {
            Token = token
        });
    }
}