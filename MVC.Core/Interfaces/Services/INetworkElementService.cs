using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Dtos;

namespace MVC.Core.Interfaces.Services;

public interface INetworkElementService
{
    Task<List<SelectListItem>> GetNetworkElementTypesAsync();
    Task<List<NetworkElementDto>> GetNetworkElementsAsync(int? parentId);
}