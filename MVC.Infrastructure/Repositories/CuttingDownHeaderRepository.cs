using Microsoft.EntityFrameworkCore;
using MVC.Core.Context;
using MVC.Core.Dtos;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Repositories;
using MVC.Infrastructure.Common;

namespace MVC.Infrastructure.Repositories;

public class CuttingDownHeaderRepository(OutagesDbContext context)
    : GenericRepository<CuttingDownHeader>(context), ICuttingDownHeaderRepository
{
    public async Task<List<ResultDto>> SearchForCuttingsAsync(CuttingsSearchDto searchDto)
    {
        var result = await context.ResultDtos
            .FromSqlRaw("fta.SP_Search_For_Cuttings @p0, @p1, @p2, @p3, @p4",
                searchDto.ChannelId ?? (object)DBNull.Value,
                searchDto.IsActive ?? (object)DBNull.Value,
                searchDto.NetworkElementTypeId ?? (object)DBNull.Value,
                searchDto.ProblemTypeId ?? (object)DBNull.Value,
                searchDto.SearchKey ?? (object)DBNull.Value)
            .ToListAsync();
        return result;
    }
    public async Task<List<IgnoredCuttingsDto>> GetIgnoredAsync()
    {
        return await context.CuttingDownIgnoreds.Where(x => true)
            .Select(x => new IgnoredCuttingsDto()
            {
                Id = x.Id,
                ActualCreate = x.ActualCreateDate,
                CabinName = x.CabinName,
                CableName = x.CableName,
                SyncCreate = (DateOnly)x.SynchDate
            })
            .ToListAsync();
    }

    public async Task<List<NetworkElementDto>> GetNetworkElementsAsync2()
    {
        var result = await context.NetworkElements
            .Where(x => true)
            .Select(x => new NetworkElementDto()
            {
                Id = x.Id,
                NetworkElementTypeId = x.NetworkElementTypeId,
                NetworkElementName = x.Name,
                ParentElementId = x.ParentNetworkElementId,
                Children = new List<NetworkElementDto>()
            })
            .ToListAsync();
        var elementDictionary = result
            .ToDictionary( x=>x.Id);

        var rootElements = new List<NetworkElementDto>();

        foreach (var element in result)
        {
            if (element.ParentElementId.HasValue &&
                elementDictionary.TryGetValue(element.ParentElementId.Value, out var parent))
            {
                parent.Children.Add(element);
            }
            else
            {
                rootElements.Add(element);
            }
        }

        return rootElements;
    }
    public async Task<List<NetworkElementDto>> GetNetworkElementsAsync(int? parentId)
    {
        var rootElements = await context.NetworkElements
            .Where(x => x.ParentNetworkElementId == parentId)
            .Select(x => new NetworkElementDto
            {
                Id = x.Id,
                NetworkElementTypeId = x.NetworkElementTypeId,
                NetworkElementName = x.Name,
                ParentElementId = x.ParentNetworkElementId,
                Children = new List<NetworkElementDto>()
            })
            .ToListAsync();

        return rootElements;
    }

}