using Apis.Core.Entities;
using Apis.Core.Interfaces.Common;
using Apis.Core.Interfaces.Services;

namespace Apis.Infrastructure.Services;

public class CuttingAService(IUnitOfWork unitOfWork) : ICuttingAService
{
    public async Task<bool> GenerateCabinCuttingsAsync()
    {
        if (await unitOfWork.CuttingDownARepository.ExistsAsync(x => true))
            return false;
        var problemTypes = await unitOfWork.StaProblemTypeRepository.GetAllAsync();
        var cabinFirstItem = await unitOfWork.CabinRepository.GetAsync(x => true);
        var cabinSequenceStart = int.Parse(cabinFirstItem.Name.Split('-')[1]);
        var cuttings = new List<CuttingDownA>();
        var baseCreateDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-7);
        for (var i = 0; i < 50; i++)
        {
            var cutting = new CuttingDownA();
            var randomCabinIdentifier = new Random().Next(cabinSequenceStart, cabinSequenceStart + 700);
            var randomCabinQualifier = new Random().Next(1, 3);
            var cabinName = $"Cabin-{randomCabinIdentifier}-{randomCabinQualifier}";
            cutting.CuttingDownCabinName = cabinName;

            var randomCreateDateAddition = new Random().Next(1, 8);
            var createDate = baseCreateDate.AddDays(randomCreateDateAddition);
            cutting.CreateDate = createDate;

            var randomEndDateAddition = new Random().Next(0, 20);
            DateOnly? endDate = createDate.AddDays(randomEndDateAddition) > DateOnly.FromDateTime(DateTime.Now)
                ? null
                : createDate.AddDays(randomEndDateAddition);
            cutting.EndDate = endDate;

            var randomGlobalIdentifier = new Random().Next(0, 2);
            var randomPlannedIdentifier = new Random().Next(0, 2);
            var isGlobal = randomGlobalIdentifier == 0;
            var isPlanned = randomPlannedIdentifier == 0;
            cutting.IsGlobal = isGlobal;
            cutting.IsPlanned = isPlanned;
            cutting.IsActive = endDate == null || endDate > DateOnly.FromDateTime(DateTime.Now);

            var randomPlanStartIdentifier = new Random().Next(-2, 1);
            cutting.PlannedStartDatetime = isPlanned ? createDate.AddDays(randomPlanStartIdentifier) : null;

            var randomEndPlanEndIdentifier = new Random().Next(0, 3);
            cutting.PlannedEndDatetime =
                isPlanned ? createDate.AddDays(randomEndDateAddition + randomEndPlanEndIdentifier) : null;

            var randomProblemTypeSelector = new Random().Next(0, problemTypes.Count());
            cutting.ProblemTypeId = problemTypes[randomProblemTypeSelector].Id;

            cuttings.Add(cutting);
        }

        unitOfWork.CuttingDownARepository.AddRange(cuttings);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}