using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Core.Context;
using MVC.Core.Dtos;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Services;
using MVC.Core.Payloads;
using MVC.Presentation.Models;

namespace MVC.Presentation.Controllers;

[Authorize]
public class CuttingController(
    IChannelService channelService,
    IProblemTypeService problemTypeService,
    INetworkElementService networkElementService,
    ICuttingsService cuttingsService,
    IHiearchyService hiearchyService
)
    : Controller
{
    public async Task<IActionResult> Master()
    {
        var model = new GetCuttingViewModel
        {
            Channels = await channelService.GetChannelsAsync(),
            ProblemTypes = (await problemTypeService.GetProblemTypesAsync()),
            NetworkElementTypes = await networkElementService.GetNetworkElementTypesAsync(),
            Results = new List<ResultDto>() // Empty results for now
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SearchForCuttings(SearchCriteriaViewModel model)
    {
        var model2 = new GetCuttingViewModel
        {
            Channels = await channelService.GetChannelsAsync(),
            ProblemTypes = (await problemTypeService.GetProblemTypesAsync()),
            NetworkElementTypes = await networkElementService.GetNetworkElementTypesAsync(),
            Results = await cuttingsService.SearchForCuttingsAsync(new CuttingsSearchDto()
            {
                ChannelId = model.ChannelId,
                IsActive = model.IsActive,
                SearchKey = model.SearchKey,
                NetworkElementTypeId = model.NetworkElementTypeId,
                ProblemTypeId = model.ProblemTypeId
            })
        };
        return View("Master", model2);
    }

    [HttpGet]
    public async Task<IActionResult> AddCutting()
    {
        var networkElementDtos = await networkElementService.GetNetworkElementsAsync(null);
        var problemTypes = (await problemTypeService.GetProblemTypesAsync());
        var networkElementTypes = await networkElementService.GetNetworkElementTypesAsync();
        var hierarchyTypes = await hiearchyService.GetHiearchyPathsListAsync();
        return View(new AddCuttingViewModel()
        {
            NetworkElementDtos = networkElementDtos,
            ProblemTypes = problemTypes,
            NetworkElementTypes = networkElementTypes,
            HierarchyTypes = hierarchyTypes
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddCutting(AddCuttingViewModel model)
    {
        await cuttingsService.AddCuttingAsync(model.HiearchyAbb, model.StartDate, model.ProblemTypeId,
            model.NetworkElementId);
        var networkElementDtos = await networkElementService.GetNetworkElementsAsync(null);
        var problemTypes = (await problemTypeService.GetProblemTypesAsync());
        var networkElementTypes = await networkElementService.GetNetworkElementTypesAsync();
        var hierarchyTypes = await hiearchyService.GetHiearchyPathsListAsync();
        return View(new AddCuttingViewModel()
        {
            NetworkElementDtos = networkElementDtos,
            ProblemTypes = problemTypes,
            NetworkElementTypes = networkElementTypes,
            HierarchyTypes = hierarchyTypes
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetChildren(int parentId)
    {
        var children = await networkElementService.GetNetworkElementsAsync(parentId);

        return Json(children);
    }

    [HttpGet]
    public async Task<IActionResult> GetCuttingsById(int elementId)
    {
        var cuttings = await cuttingsService.GetCuttingsByNetwrokElementId(elementId);
        return Json(cuttings);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCutting(int id)
    {
        await cuttingsService.DeleteCuttingAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> CloseCutting(int id)
    {
        var manualUserId = await cuttingsService.CloseCuttingAsync(id);

        return Json(new
        {
            updatedSystemUserId = manualUserId,
        });
    }

    [HttpPost]
    public async Task<IActionResult> CloseAllCuttings([FromBody] CloseAllCuttingsRequestPayload request)
    {
        var manualUserId = await cuttingsService.CloseAllCuttingsAsync(request);

        return Json(new
        {
            updatedSystemUserId = manualUserId,
        });
    }
}