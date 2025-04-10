using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIntegrationChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransformationController(ITransformationService service) : Controller
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetTransformation()
        {
            var transformation = await service.GetAllTransformationAsync();
            return Ok(transformation);
        }
    }
}
