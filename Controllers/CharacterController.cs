using dotnet_rpg.Dtos.Character;
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
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> GetAll()
        {
            return Ok(await _characterService.getAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> GetOne(int id)
        {
            var response = await _characterService.getCharacterById(id);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> Post(
            CharacterRequestDto newCharacter)
        {
            return Ok(await _characterService.saveCharacter(newCharacter));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> Put(
            int id, CharacterRequestDto newCharacter)
        {
            var response = await _characterService.UpdateCharacter(id, newCharacter);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }
    }
}