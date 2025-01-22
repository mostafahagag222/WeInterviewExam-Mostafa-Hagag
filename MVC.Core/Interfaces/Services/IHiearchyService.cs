using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Core.Interfaces.Services;

public interface IHiearchyService
{
    Task<List<SelectListItem>> GetHiearchyPathsListAsync();
}