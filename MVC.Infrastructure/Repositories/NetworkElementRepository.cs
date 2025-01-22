using Microsoft.EntityFrameworkCore;
using MVC.Core.Context;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Repositories;
using MVC.Infrastructure.Common;

namespace MVC.Infrastructure.Repositories;

public class NetworkElementRepository(OutagesDbContext context) : GenericRepository<NetworkElement>(context), INetworkElementRepository
{
    public async Task<int> GetAffectedCustomersAsync(int modelNetworkElementId)
    {
        var result = await context.AffectedCustomersDtos
            .FromSqlRaw($"fta.SP_Get_Affected_Customers {modelNetworkElementId}")
            .ToListAsync();
        return result.Select(x => x.AffectedCustomers).FirstOrDefault();
    }
}