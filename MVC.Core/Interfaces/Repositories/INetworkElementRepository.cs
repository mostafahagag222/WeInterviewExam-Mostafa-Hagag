using MVC.Core.Entities;
using MVC.Core.Interfaces.Common;

namespace MVC.Core.Interfaces.Repositories;

public interface INetworkElementRepository : IGenericRepository<NetworkElement>
{
    Task<int> GetAffectedCustomersAsync(int modelNetworkElementId);
}