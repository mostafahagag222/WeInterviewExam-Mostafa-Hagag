using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Presentation.Models;

public class SearchCriteriaViewModel
{
    public int? ChannelId { get; set; }
    public int? ProblemTypeId { get; set; }
    public bool? IsActive { get; set; }
    public int? NetworkElementTypeId { get; set; }
    public string SearchKey { get; set; }
    public List<SelectListItem> Channels { get; set; }
    public List<SelectListItem> ProblemTypes { get; set; }
    public List<SelectListItem> NetworkElementTypes { get; set; }
}
