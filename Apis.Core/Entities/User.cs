using System.ComponentModel.DataAnnotations.Schema;

namespace Apis.Core.Entities;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public byte[] HashedPassword { get; set; }

    public virtual ICollection<CuttingDownHeader> CuttingDownHeaderCreatedSystemUsers { get; set; } = new List<CuttingDownHeader>();

    public virtual ICollection<CuttingDownHeader> CuttingDownHeaderUpdatedSystemUsers { get; set; } = new List<CuttingDownHeader>();

    public virtual ICollection<CuttingDownIgnored> CuttingDownIgnoreds { get; set; } = new List<CuttingDownIgnored>();
    public byte[] Salt { get; set; }
   [NotMapped] public string Password { get; set; }
}
