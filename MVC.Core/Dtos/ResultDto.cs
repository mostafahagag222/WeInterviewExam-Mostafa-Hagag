namespace MVC.Core.Dtos;

public class ResultDto
{
    public int CuttingDownHeaderId { get; set; }
    public int CuttingDownDetailId { get; set; }
    public int ChannelId { get; set; }
    public string ChannelName { get; set; }
    public bool IsPlanned { get; set; }
    public bool IsGlobal { get; set; }
    public DateOnly? PlannedStart { get; set; }
    public DateOnly? PlannedEnd { get; set; }
    public int? CreatedUserId { get; set; }
    public string CreatedUserName { get; set; }
    public int? UpdatedUserId { get; set; }
    public string UpdatedUserName { get; set; }
    public bool IsActive { get; set; }
    public DateOnly ActualCreateDate { get; set; }
    public DateOnly? ActualEndDate { get; set; }
    public int NetWorkElementId { get; set; }
    public string NetworkElementName { get; set; }
    public int ImpactedCustomers { get; set; }
    
}
