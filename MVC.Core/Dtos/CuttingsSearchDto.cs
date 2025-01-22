namespace MVC.Core.Dtos;

public class CuttingsSearchDto
{
    public int? ChannelId { get; set; }
    public int? ProblemTypeId { get; set; }
    public bool? IsActive { get; set; }
    public int? NetworkElementTypeId { get; set; }
    public string SearchKey { get; set; }
}