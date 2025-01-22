using MVC.Core.Dtos;
using MVC.Core.Payloads;

namespace MVC.Core.Interfaces.Services;

public interface ICuttingsService
{
    Task<List<ResultDto>> SearchForCuttingsAsync(CuttingsSearchDto searchDto);
    Task<List<IgnoredCuttingsDto>> GetIgnoredCuttingsAsync();
    Task AddCuttingAsync(string modelHiearchyAbb, DateOnly modelStartDate, int modelProblemTypeId, int modelNetworkElementId);
    Task<List<CuttingsForAddDto>> GetCuttingsByNetwrokElementId(int elementId);
    Task DeleteCuttingAsync(int id);
    Task<int> CloseCuttingAsync(int id);
    Task<int> CloseAllCuttingsAsync(CloseAllCuttingsRequestPayload request);
}