using MVC.Core.Dtos;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Services;
using MVC.Core.Payloads;

namespace MVC.Infrastructure.Services;

public class CuttingsService(IUnitOfWork unitOfWork) : ICuttingsService
{
    public async Task<List<ResultDto>> SearchForCuttingsAsync(CuttingsSearchDto searchDto)
    {
        return await unitOfWork.CuttingDownHeaderRepository.SearchForCuttingsAsync(searchDto);
    }

    public async Task<List<IgnoredCuttingsDto>> GetIgnoredCuttingsAsync()
    {
        return await unitOfWork.CuttingDownHeaderRepository.GetIgnoredAsync();
    }


    public async Task AddCuttingAsync(string modelHiearchyAbb, DateOnly modelStartDate, int modelProblemTypeId,
        int modelNetworkElementId)
    {
        var cuttingDetail = new CuttingDownDetail();
        var cuttingHeader = new CuttingDownHeader();
        var channels = await unitOfWork.ChannelRepository.GetAllAsync();
        cuttingHeader.ChannelId = modelHiearchyAbb == "Governrate -> Individual Subscription"
            ? channels.Where(x => x.Name == "Source A").Select(x => x.Id).FirstOrDefault()
            : channels.Where(x => x.Name == "Source B").Select(x => x.Id).FirstOrDefault();
        cuttingHeader.IsActive = modelStartDate <= DateOnly.FromDateTime(DateTime.Now);
        cuttingHeader.IsGlobal = false;
        cuttingHeader.IsPlanned = false;
        cuttingHeader.ActualCreateDate = modelStartDate;
        var users = await unitOfWork.UserRepository.GetAllAsync();
        cuttingHeader.CreatedSystemUserId = users
            .Where(x => x.Name == "Manual")
            .Select(x => x.Id)
            .FirstOrDefault();
        cuttingHeader.ActualEndDate = null;
        cuttingHeader.UpdatedSystemUserId = null;
        cuttingHeader.CuttingDownProblemId = modelProblemTypeId;
        cuttingHeader.CuttingDownIncidentId = null;
        cuttingHeader.SynchUpdateDate = null;
        cuttingHeader.SynchCreateDate = DateOnly.FromDateTime(DateTime.Now);
        cuttingHeader.PlannedStartDateyime = null;
        cuttingHeader.PlannedEndDatetime = null;
        cuttingDetail.ActualCreateDate = modelStartDate;
        cuttingDetail.ActualEndDate = null;
        cuttingDetail.ImpactedCustomers =
            await unitOfWork.NetworkElementRepository.GetAffectedCustomersAsync(modelNetworkElementId);
        cuttingDetail.NetworkElementId = modelNetworkElementId;
        cuttingDetail.CuttingDownHeader = cuttingHeader;
        await unitOfWork.CuttingDownDetailRepository.AddAsync(cuttingDetail);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<List<CuttingsForAddDto>> GetCuttingsByNetwrokElementId(int elementId)
    {
        return await unitOfWork.GetCuttingsForAddAsync(elementId);
    }

    public async Task DeleteCuttingAsync(int id)
    {
        var cuttingDetail = await unitOfWork.CuttingDownDetailRepository.GetByIdAsync(id);
        var cuttingDetailHeader = await unitOfWork.CuttingDownHeaderRepository.GetByIdAsync(cuttingDetail.CuttingDownHeaderId);
        unitOfWork.CuttingDownDetailRepository.DeleteAsync(cuttingDetail);
        unitOfWork.CuttingDownHeaderRepository.DeleteAsync(cuttingDetailHeader);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<int> CloseCuttingAsync(int id)
    {
        var detail = await unitOfWork.CuttingDownDetailRepository.GetAsync(x => x.CuttingDownHeaderId == id);
        var header = await unitOfWork.CuttingDownHeaderRepository.GetAsync(x => x.Id == id);

        detail.ActualEndDate = DateOnly.FromDateTime(DateTime.Now);
        unitOfWork.CuttingDownDetailRepository.Update(detail);
        var users = await unitOfWork.UserRepository.GetAllAsync();
        var manualUserId = users
            .Where(x => x.Name == "Manual")
            .Select(x => x.Id)
            .FirstOrDefault();
        header.IsActive = false;
        header.ActualEndDate = DateOnly.FromDateTime(DateTime.Now);
        header.UpdatedSystemUserId = manualUserId;
        unitOfWork.CuttingDownHeaderRepository.Update(header);
        await unitOfWork.SaveChangesAsync();
        return manualUserId;
    }

    public async Task<int> CloseAllCuttingsAsync(CloseAllCuttingsRequestPayload request)
    {
        var cuttingDownDetails = await unitOfWork.CuttingDownDetailRepository.GetManyAsync(x =>
            request.HeaderIds.Contains(x.CuttingDownHeaderId)
            && x.CuttingDownHeader.IsActive);
        var cuttingDownHeaders = await unitOfWork.CuttingDownHeaderRepository.GetManyAsync(x => request.HeaderIds.Contains(x.Id)
            && x.IsActive);
        foreach (var detail in cuttingDownDetails)
        {
            detail.ActualEndDate = DateOnly.FromDateTime(DateTime.Now);
            unitOfWork.CuttingDownDetailRepository.Update(detail);
        }

        var users = await unitOfWork.UserRepository.GetAllAsync();
        var manualUserId = users
            .Where(x => x.Name == "Manual")
            .Select(x => x.Id)
            .FirstOrDefault();
        foreach (var header in cuttingDownHeaders)
        {
            header.IsActive = false;
            header.ActualEndDate = DateOnly.FromDateTime(DateTime.Now);
            header.UpdatedSystemUserId = manualUserId;
            unitOfWork.CuttingDownHeaderRepository.Update(header);
        }

        await unitOfWork.SaveChangesAsync();
        return manualUserId;
    }
}