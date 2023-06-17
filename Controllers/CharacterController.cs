using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            this._characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> GetAll()
        {
            return Ok(await _characterService.getAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetOne(int id)
        {
            return Ok(await _characterService.getCharacterById(id));   
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> Post(Character newCharacter) {
            return Ok(await _characterService.saveCharacter(newCharacter));
        }
    }
}