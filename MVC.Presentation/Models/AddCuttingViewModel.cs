using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Dtos;
using MVC.Core.Entities;

namespace MVC.Presentation.Models;

public class AddCuttingViewModel
{
    public List<SelectListItem> HierarchyTypes { get; set; } = new();
    public List<SelectListItem> NetworkElementTypes { get; set; } = new();
    public List<SelectListItem> ProblemTypes { get; set; } = new();
    public List<NetworkElementDto> NetworkElementDtos { get; set; }
    public int NetworkElementId { get; set; }
    public DateOnly StartDate { get; set; }
    public int ProblemTypeId { get; set; }
    public string HiearchyAbb { get; set; }
}