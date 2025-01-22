using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class GovernrateService(IUnitOfWork unitOfWork) : IGovernrateService
{
    public async Task AddGovernratesAsync()
    {
        Console.WriteLine("Checking For Governrates...");
        if (await unitOfWork.GovernrateRepository.ExistsAsync(x => true))
        {
            Console.WriteLine("Governrate already exists!");
            return;
        }

        Console.WriteLine("Adding Governrates and sectors...");

        var egyptGovernorates = new[]
        {
            "Cairo",
            "Giza",
            "Alexandria",
            "Beni Suef",
        }.Select(x => new Governrate
        {
            Name = x,
            Sectors = new List<Sector>
            {
                new()
                {
                    Name = $"{x}-North",
                },
                new()
                {
                    Name = $"{x}-South",
                },
                new()
                {
                    Name = $"{x}-East",
                },
                new()
                {
                    Name = $"{x}-West",
                }
            }
        });

        unitOfWork.GovernrateRepository.AddRange(egyptGovernorates);
        await unitOfWork.SaveChangesAsync();
    }
}