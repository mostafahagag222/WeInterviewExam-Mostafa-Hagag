using MVC.Core.Context;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Repositories;
using MVC.Infrastructure.Common;

namespace MVC.Infrastructure.Repositories;

public class NetworkElementTypeRepository(OutagesDbContext context)
    : GenericRepository<NetworkElementType>(context), INetworkElementTypeRepository
{
}