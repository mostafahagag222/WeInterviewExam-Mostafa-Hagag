using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class NetworkElementHierarchyPathService(IUnitOfWork unitOfWork) : INetworkElementHierarchyPathService
{
    public async Task CreateNetworkElementHierarchyPathsAsync()
    {
        if (await unitOfWork.NetworkElementHierarchyRepository.ExistsAsync(x => true))
        {
            Console.WriteLine("Network element hierarchy path exists!");
            return;
        }

        Console.WriteLine("Creating network element hierarchy paths...");
        var paths = new List<NetworkElementHierarchyPath>()
        {
            new()
            {
                Name =
                    "Governrate, Sector, Zone, City, Station, Tower, Cabin, Cable, Buidling, Flat, Individual Subscription",
                Abbreviation = "Governrate -> Individual Subscription"
            },
            new()
            {
                Name = "Governrate, Sector, Zone, City, Station, Tower, Cabin, Cable, Buidling, Corporate Subscription",
                Abbreviation = "Governrate -> Corporate Subscription"
            }
        };
        unitOfWork.NetworkElementHierarchyRepository.AddRange(paths);
        await unitOfWork.SaveChangesAsync();
        Console.WriteLine("Network element hierarchy paths created!");
    }
}