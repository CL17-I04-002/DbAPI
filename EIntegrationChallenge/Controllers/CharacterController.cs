using Application.Interfaces;
using Domain.Interfaces.ExternalServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EIntegrationChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CharacterController(ICharacterService service, IDbApi dbApiClient) : Controller
    {

        [HttpPost("sync")]
        public async Task<ActionResult> Create()
        {
           var allCharacter = await dbApiClient.SyncDataFromExternalService();
            if (allCharacter == 0) return Conflict("You must clean data before synchronizing");
            else if(allCharacter == -1) return Conflict("There was an error synchronizing the data");
            else return Ok("Data was synchronized");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            var character = await service.GetCharacterByIdAsync(id);
            if (character == null) return NotFound("Character not found");
            return Ok(character);
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await service.GetAllCharactersAsync(); 
            return Ok(characters);

        }
    }
}
