using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class ChannelService(IUnitOfWork unitOfWork) : IChannelService
{
    public async Task CreateChannels()
    {
        if (await unitOfWork.ChannelRepository.ExistsAsync(x => true))
        {
            Console.WriteLine("Channels already exists");
            return;
        }

        Console.WriteLine("Creating channels........");
        var channels = new List<Channel>()
        {
            new()
            {
                Name = "Source A",
            },
            new()
            {
                Name = "Source B",
            }
        };
        unitOfWork.ChannelRepository.AddRange(channels);
        await unitOfWork.SaveChangesAsync();

        Console.WriteLine("Channels created");
    }
}