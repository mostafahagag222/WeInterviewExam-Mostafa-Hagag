using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Core.Context;
using MVC.Core.Dtos;

namespace MVC.Presentation.Controllers;

public class NetworkElementController(OutagesDbContext context) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetHiearchyPath(string searchValue)
    {
        var elements = await context.NetworkElementDtos
            .FromSqlRaw($"fta.SP_GetHierarchyPath '{searchValue}'")
            .ToListAsync();
        var parents = new List<NetworkElementDto>();
        foreach (var element in elements)
        {
            if (element.ParentElementId == null)
                parents.Add(element);
            else
            {
                if (elements.FirstOrDefault(x => x.Id == element.ParentElementId) != null)
                    elements.FirstOrDefault(x => x.Id == element.ParentElementId)?.Children.Add(element);
            }
        }

        return Json(parents.FirstOrDefault());
    }
}