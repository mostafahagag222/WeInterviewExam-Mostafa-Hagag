using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Dtos;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Services;

namespace MVC.Infrastructure.Services;

public class NetworkElementService(IUnitOfWork unitOfWork) : INetworkElementService
{
    public async Task<List<SelectListItem>> GetNetworkElementTypesAsync()
    {
        var r = await unitOfWork.NetworkElementTypeRepository.GetAllListAsync();
            return r
            .Select(x =>
            new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
    }

    public async Task<List<NetworkElementDto>> GetNetworkElementsAsync(int? parentId)
    {
        return await unitOfWork.CuttingDownHeaderRepository.GetNetworkElementsAsync(parentId);

    }
}