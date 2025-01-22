using System.Diagnostics.CodeAnalysis;
using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class SubscriptionService(IUnitOfWork unitOfWork) : ISubscriptionService
{
    public async Task AddSubscriptionsAsync()
    {
        Console.WriteLine("Checking for Individual subscriptions...");
        if (await unitOfWork.SubscriptionRepository.ExistsAsync(x => x.FlatId > 0))
            Console.WriteLine("There are already individual subscriptions");
        else
        {
            Console.WriteLine("Adding individual subscription...");
            var flats = await unitOfWork.FlatRepository.GetAllAsync();
            foreach (var flat in flats)
            {
                flat.Subscribtions = new List<Subscribtion>
                {
                    new()
                    {
                        BuildingId = flat.BuildingId,
                        Name = $"Subscription-f-{flat.Id}"
                    }
                };
            }

            Console.WriteLine("Adding individual subscriptions...");
            await unitOfWork.SaveChangesAsync();
            Console.WriteLine("Individual Subscriptions added");
        }
    }

    public async Task AddCorporateSubscriptionsAsync()
    {
        Console.WriteLine("Checking for Corporate subscriptions...");
        if (await unitOfWork.SubscriptionRepository.ExistsAsync(x => x.FlatId == null))
            Console.WriteLine("There are already Corporate subscriptions");
        else
        {
            Console.WriteLine("Adding Corporate subscription...");
            var buildings = await unitOfWork.BuildingRepository.GetAllAsync();
            foreach (var building in buildings)
            {
                building.Subscribtions = new List<Subscribtion>
                {
                    new()
                    {
                        Name = $"Subscription-b-{building.Id}"
                    }
                };
            }

            Console.WriteLine("Adding Corporate subscriptions...");
            await unitOfWork.SaveChangesAsync();
            Console.WriteLine("Corporate Subscriptions added");
        }
    }
}