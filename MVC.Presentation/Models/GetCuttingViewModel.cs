using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Dtos;

namespace MVC.Presentation.Models;

public class GetCuttingViewModel
{
    public List<SelectListItem> Channels { get; set; }
    public List<SelectListItem> ProblemTypes { get; set; }
    public List<SelectListItem> NetworkElementTypes { get; set; }
    public List<ResultDto> Results { get; set; }
}
