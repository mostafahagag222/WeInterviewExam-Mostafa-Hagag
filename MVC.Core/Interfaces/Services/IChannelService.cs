using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Dtos;

namespace MVC.Core.Interfaces.Services;

public interface IChannelService
{
    Task<List<SelectListItem>> GetChannelsAsync();
}