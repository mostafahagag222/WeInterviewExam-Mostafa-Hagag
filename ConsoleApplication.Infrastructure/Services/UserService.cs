using WeInterviewExam.Core;
using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async Task AddUsersAsync()
    {
        Console.WriteLine("Checking for users...");

        if (await unitOfWork.UserRepository.ExistsAsync(x => true))
        {
            Console.WriteLine("Users already exists!");
            return;
        }

        Console.WriteLine("Adding users...");

        var users = new List<User>
        {
            new()
            {
                Name = "Admin",
                Password = "Admin"
            },
            new()
            {
                Name = "SourceA",
                Password = "SourceA"
            },
            new()
            {
                Name = "SourceB",
                Password = "SourceB"
            },
            new()
            {
                Name = "Manual",
                Password = "Manual"
            },
        };
        foreach (var user in users)
        {
            user.Salt = Guid.NewGuid().ToByteArray();
            user.HashedPassword = user.Password.HashPassword(user.Salt);
        }

        unitOfWork.UserRepository.AddRange(users);
        await unitOfWork.SaveChangesAsync();
    }
}