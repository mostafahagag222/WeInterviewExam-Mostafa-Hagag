namespace MVC.Core.Dtos;

public class IgnoredCuttingsDto
{
    public int Id { get; set; }
    public DateOnly ActualCreate { get; set; }
    public DateOnly SyncCreate { get; set; }
    public string CableName { get; set; }
    public string CabinName { get; set; }
}