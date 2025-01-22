using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Dtos;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Services;

namespace MVC.Infrastructure.Services;

public class ProblemTypeService(IUnitOfWork unitOfWork) : IProblemTypeService
{
    public async Task<List<SelectListItem>> GetProblemTypesAsync()
    {
        var problemTypes = await unitOfWork.FtaProblemTypeRepository.GetAllListAsync();
        return problemTypes.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
    }
}