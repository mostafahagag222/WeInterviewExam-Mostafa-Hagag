using System.Text.Json.Serialization;

namespace MVC.Core.Payloads;

public class CloseAllCuttingsRequestPayload
{
    public List<int> HeaderIds { get; set; }
}