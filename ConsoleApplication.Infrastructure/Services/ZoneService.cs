using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class  ZoneService(IUnitOfWork unitOfWork) : IZoneService
{
    public async Task AddZonesAsync()
    {
        Console.WriteLine("Checking For Zones...");
        if (await unitOfWork.ZoneRepository.ExistsAsync(x => true))
        {
            Console.WriteLine("Zones Table already has data!");
            return;
        }

        Console.WriteLine("Adding Zones and Cities...");
        
        var sectors = await unitOfWork.SectorRepository.GetAllAsync();
        foreach (var sector in sectors)
        {
            sector.Zones = new List<Zone>
            {
                new()
                {
                    Name = $"Zone-{sector.Id}-1",
                    Cities = new List<City>
                    {
                        new()
                        {
                            Name = $"City-{sector.Id}-1-1",
                        },
                        new()
                        {
                            Name = $"City-{sector.Id}-1-2",
                        }
                    }
                },
                new()
                {
                    Name = $"Zone-{sector.Id}-2",
                    Cities = new List<City>
                    {
                        new()
                        {
                            Name = $"City-{sector.Id}-2-1",
                        },
                        new()
                        {
                            Name = $"City-{sector.Id}-2-2",
                        }
                    }
                }
            };
        }

        await unitOfWork.SaveChangesAsync();
    }
}