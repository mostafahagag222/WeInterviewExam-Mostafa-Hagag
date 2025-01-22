namespace WeInterviewExam.Core.Entities;

public class Flat
{
    public int Id { get; set; }

    public int BuildingId { get; set; }
    public string Name { get; set; }

    public virtual Building Building { get; set; }

    public virtual ICollection<Subscribtion> Subscribtions { get; set; } = new List<Subscribtion>();
}
