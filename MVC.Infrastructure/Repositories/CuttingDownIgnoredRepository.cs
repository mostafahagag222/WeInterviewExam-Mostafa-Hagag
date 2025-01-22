using MVC.Core.Context;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Repositories;
using MVC.Infrastructure.Common;

namespace MVC.Infrastructure.Repositories;

public class CuttingDownIgnoredRepository(OutagesDbContext context) : GenericRepository<CuttingDownIgnored>(context), ICuttingDownIgnoredRepository
{
    
}