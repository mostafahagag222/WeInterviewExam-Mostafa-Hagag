using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class CabinService(IUnitOfWork unitOfWork) : ICabinService
{
    public async Task AddCabinsAsync()
    {
        Console.WriteLine("Checking For Cabins...");
        if (await unitOfWork.CabinRepository.ExistsAsync(x => true))
        {
            Console.WriteLine("Cabins Table already has data!");
            return;
        }

        Console.WriteLine("Adding Cabins and Cables...");
        
        var towers = await unitOfWork.TowerRepository.GetAllAsync();
        foreach (var tower in towers)
        {
            tower.Cabins = new List<Cabin>
            {
                new()
                {
                    Name = $"Cabin-{tower.Id}-1",
                    Cables = new List<Cable>
                    {
                        new()
                        {
                            Name = $"Cable-{tower.Id}-1-1",
                        },
                        new()
                        {
                            Name = $"Cable-{tower.Id}-1-2",
                        }
                    }
                },
                new()
                {
                    Name = $"Cabin-{tower.Id}-2",
                    Cables = new List<Cable>
                    {
                        new()
                        {
                            Name = $"Cable-{tower.Id}-2-1",
                        },
                        new()
                        {
                            Name = $"Cable-{tower.Id}-2-2",
                        }
                    }
                }
            };
        }

        await unitOfWork.SaveChangesAsync();
    }
}