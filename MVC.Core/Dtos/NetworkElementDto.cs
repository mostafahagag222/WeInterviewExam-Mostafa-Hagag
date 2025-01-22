using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Core.Dtos;

public class NetworkElementDto
{
    public int NetworkElementTypeId { get; set; }
    public string NetworkElementName { get; set; }
    public int? ParentElementId { get; set; }
    [NotMapped] public List<NetworkElementDto> Children { get; set; } = new();
    [NotMapped] string TargetElementName { get; set; }
    public int Id { get; set; }
}