using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class BlockService(IUnitOfWork unitOfWork) : IBlockService
{
    public async Task AddBlocksAsync()
    {
        Console.WriteLine("Checking For Blocks...");
        if (await unitOfWork.BlockRepository.ExistsAsync(x => true))
        {
            Console.WriteLine("Blocks Table already has data!");
            return;
        }

        Console.WriteLine("Adding Blocks and Cities...");

        var cables = await unitOfWork.CableRepository.GetAllAsync();
        foreach (var cable in cables)
        {
            cable.Blocks = new List<Block>
            {
                new()
                {
                    Name = $"Block-{cable.Id}-1",
                    Buildings = new List<Building>
                    {
                        new()
                        {
                            Name = $"Building-{cable.Id}-1-1",
                            Flats = new List<Flat>
                            {
                                new()
                                {
                                    Name = $"Flat-{cable.Id}-1-1-1",
                                },
                                new()
                                {
                                    Name = $"Flat-{cable.Id}-1-1-2",
                                }
                            }
                        },
                        new()
                        {
                            Name = $"Building-{cable.Id}-1-2",
                            Flats = new List<Flat>
                            {
                                new()
                                {
                                    Name = $"Flat-{cable.Id}-1-2-1",
                                },
                                new()
                                {
                                    Name = $"Flat-{cable.Id}-1-2-2",
                                }
                            }
                        }
                    }
                },
                new()
                {
                    Name = $"Block-{cable.Id}-2",
                    Buildings = new List<Building>
                    {
                        new()
                        {
                            Name = $"Building-{cable.Id}-2-1",
                            Flats = new List<Flat>
                            {
                                new()
                                {
                                    Name = $"Flat-{cable.Id}-2-1-1",
                                },
                                new()
                                {
                                    Name = $"Flat-{cable.Id}-2-1-2",
                                }
                            }
                        },
                        new()
                        {
                            Name = $"Building-{cable.Id}-2-2",
                            Flats = new List<Flat>
                            {
                                new()
                                {
                                    Name = $"Flat-{cable.Id}-2-2-1",
                                },
                                new()
                                {
                                    Name = $"Flat-{cable.Id}-2-2-2",
                                }
                            }
                        }
                    }
                }
            };
        }

        await unitOfWork.SaveChangesAsync();
    }
}