using System.Threading.Tasks;
using Apis.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apis.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuttingsController(ICuttingAService cuttingAService,ICuttingBService cuttingBService) : ControllerBase
    {
        [HttpGet("generate/A")]
        public async Task<IActionResult> GenerateCaseACuttings()
        {
            if (await cuttingAService.GenerateCabinCuttingsAsync())
                return Ok("Generated Successfully");
            return Ok("Already Populated");
        }
        
        [HttpGet("generate/B")]
        public async Task<IActionResult> GenerateCaseBCuttings()
        {
            if (await cuttingBService.GenerateCableCuttingsAsync())
                return Ok("Generated Successfully");
            return Ok("Already Populated");
        }
    }
}