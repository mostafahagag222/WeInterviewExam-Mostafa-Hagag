using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC.Core.Context;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Services;
using MVC.Presentation.Models;

namespace MVC.Presentation.Controllers;

public class IgnoredController(
    ICuttingsService cuttingsService,
    OutagesDbContext context,
    IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetIgnored()
    {
        var model = new IgnoredCuttings()
        {
            IgnoredCuttingsList = await cuttingsService.GetIgnoredCuttingsAsync()
        };
        return View(model);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteIgnoredCutting(int id)
    {
        var cutting = await context.CuttingDownIgnoreds
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        context.CuttingDownIgnoreds.Remove(cutting);
        await context.SaveChangesAsync();
        return Ok();
    }
}