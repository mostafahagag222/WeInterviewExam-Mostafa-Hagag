using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Services;

namespace MVC.Infrastructure.Services;

public class HiearchyService(IUnitOfWork unitOfWork) : IHiearchyService
{
    public async Task<List<SelectListItem>> GetHiearchyPathsListAsync()
    {
        return await unitOfWork.HiearchyPathRepository.GetHiearchyPathsListAsync();

    }
}