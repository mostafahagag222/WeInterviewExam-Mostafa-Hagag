using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class StationService(IUnitOfWork unitOfWork) : IStationService
{
    public async Task AddStationsAsync()
    {
        Console.WriteLine("Checking For Stations...");
        if (await unitOfWork.StationRepository.ExistsAsync(x => true))
        {
            Console.WriteLine("Station Table already has data!");
            return;
        }

        Console.WriteLine("Adding Stations and Towers...");
        
        var cities = await unitOfWork.CityRepository.GetAllAsync();
        foreach (var city in cities)
        {
            city.Stations = new List<Station>
            {
                new()
                {
                    Name = $"Station-{city.Id}-1",
                    Towers = new List<Tower>
                    {
                        new()
                        {
                            Name = $"Tower-{city.Id}-1-1",
                        },
                        new()
                        {
                            Name = $"Tower-{city.Id}-1-2",
                        }
                    }
                },
                new()
                {
                    Name = $"Station-{city.Id}-2",
                    Towers = new List<Tower>
                    {
                        new()
                        {
                            Name = $"Tower-{city.Id}-2-1",
                        },
                        new()
                        {
                            Name = $"Tower-{city.Id}-2-2",
                        }
                    }
                }
            };
        }

        await unitOfWork.SaveChangesAsync();
    }
}