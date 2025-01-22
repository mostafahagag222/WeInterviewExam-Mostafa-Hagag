using MVC.Core.Context;
using MVC.Core.Dtos;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Repositories;
using MVC.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace MVC.Infrastructure.Repositories;

public class CuttingDownDetailRepository(OutagesDbContext context)
    : GenericRepository<CuttingDownDetail>(context), ICuttingDownDetailRepository
{

}