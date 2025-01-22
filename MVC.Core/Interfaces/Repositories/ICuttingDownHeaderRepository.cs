using MVC.Core.Dtos;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Common;

namespace MVC.Core.Interfaces.Repositories;

public interface ICuttingDownHeaderRepository : IGenericRepository<CuttingDownHeader>
{
    Task<List<ResultDto>> SearchForCuttingsAsync(CuttingsSearchDto searchDto);
    Task<List<IgnoredCuttingsDto>> GetIgnoredAsync();
    Task<List<NetworkElementDto>> GetNetworkElementsAsync(int? parentId);
}