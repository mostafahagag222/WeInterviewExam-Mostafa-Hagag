using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Dtos;

namespace MVC.Core.Interfaces.Services;

public interface IProblemTypeService
{
    Task<List<SelectListItem>> GetProblemTypesAsync();
}