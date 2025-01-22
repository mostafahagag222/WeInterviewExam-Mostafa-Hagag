using Apis.Core.Context;
using Apis.Core.Entities;
using Apis.Core.Interfaces.Repositories;
using Apis.Infrastructure.Common;

namespace Apis.Infrastructure.Repositories;

public class CableRepository(OutagesDbContext context) : GenericRepository<Cable>(context), ICableRepository
{
    
}