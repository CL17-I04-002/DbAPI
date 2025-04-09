using Application.Interfaces;
using Domain.Interfaces.ExternalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EIntegrationChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController(ICharacterService service, IDbApi dbApiClient) : Controller
    {

        [HttpPost("sync")]
        public async Task<ActionResult> Create()
        {
           var allCharacter = await dbApiClient.SyncDataFromExternalService();
            if (allCharacter == null) return Conflict("You must clean data before synchronizing");
            else return Ok(allCharacter);
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
            var characters = await dbApiClient.GetAllCharactersAsync(); 
            return Ok(characters);

        }
    }
}
