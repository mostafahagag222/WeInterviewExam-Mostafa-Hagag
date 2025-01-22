using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Common;

namespace MVC.Core.Interfaces.Repositories;

public interface IHiearchyPathRepository : IGenericRepository<NetworkElementHierarchyPath>
{
    Task<List<SelectListItem>> GetHiearchyPathsListAsync();
}