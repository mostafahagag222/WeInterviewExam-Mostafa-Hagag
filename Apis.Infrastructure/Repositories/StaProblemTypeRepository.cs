using Apis.Core.Context;
using Apis.Core.Entities;
using Apis.Core.Interfaces.Repositories;
using Apis.Infrastructure.Common;

namespace Apis.Infrastructure.Repositories;

public class StaProblemTypeRepository(OutagesDbContext context) : GenericRepository<StaProblemType>(context), IStaProblemTypeRepository
{
    
}