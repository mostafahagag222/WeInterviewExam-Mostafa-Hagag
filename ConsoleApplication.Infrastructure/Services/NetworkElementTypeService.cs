using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class NetworkElementTypesService(IUnitOfWork unitOfWork) : INetworkElementTypesService
{
    public async Task AddNetworkElementTypes()
    {
        if (await unitOfWork.NetworkElementTypeRepository.ExistsAsync(x => true))
        {
            Console.WriteLine("Network element types already exists");
            return;
        }

        Console.WriteLine("Fetching Paths");
        var paths = await unitOfWork.NetworkElementHierarchyRepository.GetAllAsync();
        var individualPathId = paths
            .Where(x => x.Abbreviation == "Governrate -> Individual Subscription")
            .Select(x => x.Id)
            .FirstOrDefault();
        var corporatePathId = paths
            .Where(x => x.Abbreviation != "Governrate -> Individual Subscription")
            .Select(x => x.Id)
            .FirstOrDefault();
        Console.WriteLine("Adding network element types.....");
        var networkElementType = new NetworkElementType
        {
            Name = "Governrate",
            NetworkElementHierarchyPathId = individualPathId,
            InverseParentNetworkElement = new List<NetworkElementType>
            {
                new()
                {
                    Name = "Sector",
                    NetworkElementHierarchyPathId = individualPathId,
                    InverseParentNetworkElement = new List<NetworkElementType>
                    {
                        new()
                        {
                            Name = "Zone",
                            NetworkElementHierarchyPathId = individualPathId,
                            InverseParentNetworkElement = new List<NetworkElementType>
                            {
                                new()
                                {
                                    Name = "City",
                                    NetworkElementHierarchyPathId = individualPathId,
                                    InverseParentNetworkElement = new List<NetworkElementType>
                                    {
                                        new()
                                        {
                                            Name = "Station",
                                            NetworkElementHierarchyPathId = individualPathId,
                                            InverseParentNetworkElement = new List<NetworkElementType>
                                            {
                                                new()
                                                {
                                                    Name = "Tower",
                                                    NetworkElementHierarchyPathId = individualPathId,
                                                    InverseParentNetworkElement = new List<NetworkElementType>
                                                    {
                                                        new()
                                                        {
                                                            Name = "Cabin",
                                                            NetworkElementHierarchyPathId = individualPathId,
                                                            InverseParentNetworkElement = new List<NetworkElementType>
                                                            {
                                                                new()
                                                                {
                                                                    Name = "Cable",
                                                                    NetworkElementHierarchyPathId = individualPathId,
                                                                    InverseParentNetworkElement =
                                                                        new List<NetworkElementType>
                                                                        {
                                                                            new()
                                                                            {
                                                                                Name = "Block",
                                                                                NetworkElementHierarchyPathId =
                                                                                    individualPathId,
                                                                                InverseParentNetworkElement =
                                                                                    new List<NetworkElementType>
                                                                                    {
                                                                                        new()
                                                                                        {
                                                                                            Name = "Building",
                                                                                            NetworkElementHierarchyPathId =
                                                                                                individualPathId,
                                                                                            InverseParentNetworkElement =
                                                                                                new List<
                                                                                                    NetworkElementType>
                                                                                                {
                                                                                                    new()
                                                                                                    {
                                                                                                        Name =
                                                                                                            "Flat",
                                                                                                        NetworkElementHierarchyPathId =
                                                                                                            individualPathId,
                                                                                                        InverseParentNetworkElement =
                                                                                                            new List
                                                                                                                <NetworkElementType>
                                                                                                                {
                                                                                                                    new(
                                                                                                                    )
                                                                                                                    {
                                                                                                                        Name =
                                                                                                                            "Individual Subscription",
                                                                                                                        NetworkElementHierarchyPathId =
                                                                                                                            individualPathId
                                                                                                                    },
                                                                                                                    new(
                                                                                                                    )
                                                                                                                    {
                                                                                                                        Name =
                                                                                                                            "Corporate Subscription",
                                                                                                                        NetworkElementHierarchyPathId =
                                                                                                                            corporatePathId
                                                                                                                    }
                                                                                                                }
                                                                                                    }
                                                                                                }
                                                                                        }
                                                                                    }
                                                                            }
                                                                        }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
        await unitOfWork.NetworkElementTypeRepository.AddAsync(networkElementType);
        await unitOfWork.SaveChangesAsync();
        Console.WriteLine("Added network element types");
    }
}