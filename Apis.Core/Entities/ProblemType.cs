namespace Apis.Core.Entities;

public class ProblemType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<CuttingDownHeader> CuttingDownHeaders { get; set; } = new List<CuttingDownHeader>();
}
