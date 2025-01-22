using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Dtos;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Services;

namespace MVC.Infrastructure.Services;

public class ChannelService(IUnitOfWork unitOfWork) : IChannelService
{
    public async Task<List<SelectListItem>> GetChannelsAsync()
    {
        var channels = await unitOfWork.ChannelRepository.GetAllListAsync();
            return channels.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
    }
}