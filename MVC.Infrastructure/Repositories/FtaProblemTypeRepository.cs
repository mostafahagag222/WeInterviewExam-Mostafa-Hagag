using MVC.Core.Context;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Repositories;
using MVC.Infrastructure.Common;

namespace MVC.Infrastructure.Repositories;

public class FtaProblemTypeRepository(OutagesDbContext context) : GenericRepository<FtaProblemType>(context), IFtaProblemTypeRepository
{
    
}