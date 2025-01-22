using System.Net;
using Autofac;
using Microsoft.EntityFrameworkCore;
using WeInterviewExam.Core.Context;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Presentation;

public static class App
{
    public static async Task Run(IContainer container)
    {
        await ExecuteDatabaseProcedureAsync(container, "dbo.SP_Clean");

        await ExecuteScopedServiceAsync<IUserService>(container,
            async userService => { await userService.AddUsersAsync(); });

        await PopulateNetworkElementsInSta(container);

        await ExecuteScopedServiceAsync<IProblemTypeService>(container,
            async problemTypeService => { await problemTypeService.CreateProblemTypesAsync(); });

        await ExecuteScopedServiceAsync<INetworkElementHierarchyPathService>(container,
            async networkElementHierarchyPathService =>
            {
                await networkElementHierarchyPathService.CreateNetworkElementHierarchyPathsAsync();
            });

        await ExecuteScopedServiceAsync<INetworkElementTypesService>(container,
            async networkElementTypesService => { await networkElementTypesService.AddNetworkElementTypes(); });

        await ExecuteScopedServiceAsync<IChannelService>(container,
            async channelService => { await channelService.CreateChannels(); });

        await CallApisWithSemaphore(container);

        await ExecuteDatabaseProcedures(container);

        RunIndefinitely();
    }

    private static async Task PopulateNetworkElementsInSta(IContainer container)
    {
        await ExecuteScopedServiceAsync<IGovernrateService>(container,
            async governrateService => { await governrateService.AddGovernratesAsync(); });

        await ExecuteScopedServiceAsync<IZoneService>(container,
            async zoneService => { await zoneService.AddZonesAsync(); });

        await ExecuteScopedServiceAsync<IStationService>(container,
            async stationService => { await stationService.AddStationsAsync(); });

        await ExecuteScopedServiceAsync<ICabinService>(container,
            async cabinService => { await cabinService.AddCabinsAsync(); });

        await ExecuteScopedServiceAsync<IBlockService>(container,
            async blockService => { await blockService.AddBlocksAsync(); });

        await ExecuteScopedServiceAsync<ISubscriptionService>(container,
            async subscriptionService => { await subscriptionService.AddSubscriptionsAsync(); });

        await ExecuteScopedServiceAsync<ISubscriptionService>(container,
            async subscriptionService => { await subscriptionService.AddCorporateSubscriptionsAsync(); });
    }

    private static async Task CallApisWithSemaphore(IContainer container)
    {
        var semaphore = new SemaphoreSlim(2);

        var tasks = new[]
        {
            CallApi("A"),
            CallApi("B")
        };

        await Task.WhenAll(tasks);
        return;

        async Task CallApi(string caseType)
        {
            await semaphore.WaitAsync();
            try
            {
                using var httpClient = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                });
                Console.WriteLine($"Calling API to generate cuttings of Case {caseType}");
                var response = await httpClient.GetAsync($"http://localhost:5000/api/cuttings/generate/{caseType}");
                Console.WriteLine($"API response for Case {caseType} is: {await response.Content.ReadAsStringAsync()}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred during API call for Case {caseType}: {e.Message}");
            }
            finally
            {
                semaphore.Release();
            }
        }
    }


    private static async Task ExecuteDatabaseProcedures(IContainer container)
    {
        await ExecuteDatabaseProcedureAsync(container, "fta.[SP_Transfer_Network_Elements2]",
            "Transferring Network Elements...");
        await ExecuteDatabaseProcedureAsync(container, "FTA.SP_Create", "Executing SP_Create");
        await ExecuteDatabaseProcedureAsync(container, "FTA.SP_Close", "Executing SP_Close");
    }

    private static async Task ExecuteDatabaseProcedureAsync(IContainer container, string procedureName,
        string logMessage = null)
    {
        await using var scope = container.BeginLifetimeScope();
        {
            var context = scope.Resolve<OutagesDbContext>();

            if (!string.IsNullOrEmpty(logMessage))
            {
                Console.WriteLine(logMessage);
            }

            await context.Database.ExecuteSqlRawAsync(procedureName);
        }
    }

    private static async Task ExecuteScopedServiceAsync<TService>(IContainer container, Func<TService, Task> action)
        where TService : notnull
    {
        await using var scope = container.BeginLifetimeScope();
        {
            var service = scope.Resolve<TService>();
            await action(service);
        }
    }

    private static void RunIndefinitely()
    {
        while (true)
        {
            Console.WriteLine("Running Forever.......");
            Thread.Sleep(30000);
        }
    }
}