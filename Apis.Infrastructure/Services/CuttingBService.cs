using Apis.Core.Entities;
using Apis.Core.Interfaces.Common;
using Apis.Core.Interfaces.Services;

namespace Apis.Infrastructure.Services;

public class CuttingBService(IUnitOfWork unitOfWork) : ICuttingBService
{
     public async Task<bool> GenerateCableCuttingsAsync()
    {
        if (await unitOfWork.CuttingDownBRepository.ExistsAsync(x=>true))
            return false;
        var problemTypes = await unitOfWork.StaProblemTypeRepository.GetAllAsync();
        var cableFirstItem = await unitOfWork.CableRepository.GetAsync(x => true);
        var cableSequenceStart = int.Parse(cableFirstItem.Name.Split('-')[1]);
        var cuttings = new List<CuttingDownB>();
        var baseCreateDate =DateOnly.FromDateTime(DateTime.Now).AddDays(-7);
        for (var i = 0; i < 50; i++)
        {
            var cutting = new CuttingDownB();
            var randomCableIdentifier = new Random().Next(cableSequenceStart, cableSequenceStart + 450);
            var randomCableQualifier = new Random().Next(1, 3);
            var randomCableQualifier2 = new Random().Next(1, 3);
            var cableName = $"Cable-{randomCableIdentifier}-{randomCableQualifier}-{randomCableQualifier2}";
            cutting.CuttingDownCableName = cableName;

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

        unitOfWork.CuttingDownBRepository.AddRange(cuttings);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}