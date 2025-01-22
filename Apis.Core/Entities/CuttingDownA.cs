namespace Apis.Core.Entities;

public class CuttingDownA
{
    public int Id { get; set; }

    public string CuttingDownCabinName { get; set; }

    public DateOnly CreateDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool IsPlanned { get; set; }

    public bool IsGlobal { get; set; }

    public bool IsActive { get; set; }

    public DateOnly? PlannedStartDatetime { get; set; }

    public DateOnly? PlannedEndDatetime { get; set; }

    public string CreatedUserName { get; set; }

    public string UpdatedUserName { get; set; }

    public int ProblemTypeId { get; set; }

    public virtual StaProblemType ProblemType { get; set; }
}
