using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Core.Context;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Repositories;
using MVC.Infrastructure.Common;

namespace MVC.Infrastructure.Repositories;

public class HiearchyPathRepository(OutagesDbContext context) : GenericRepository<NetworkElementHierarchyPath>(context), IHiearchyPathRepository
{
    public async Task<List<SelectListItem>> GetHiearchyPathsListAsync()
    {
        return await context.NetworkElementHierarchyPaths
            .Where(x => true)
            .Select(x =>
                new SelectListItem
                {
                    Text = x.Abbreviation,
                    Value = x.Id.ToString()
                }).ToListAsync();
    }
}